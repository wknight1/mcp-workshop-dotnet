using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MyMonkeyApp.Models;

namespace MyMonkeyApp.Helpers;

/// <summary>
/// Static helper that manages a collection of Monkey data.
/// It fetches data from a configurable MCP endpoint, caches results,
/// and exposes methods to query and pick monkeys.
/// </summary>
public static class MonkeyHelper
{
    // Default public dataset (can be overridden by specifying a url)
    private const string DefaultMonkeysUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/monkeys.json";

    private static readonly HttpClient _httpClient = new();
    private static readonly SemaphoreSlim _fetchLock = new(1, 1);

    private static List<Monkey> _cache = new();
    private static DateTime _lastFetch = DateTime.MinValue;

    // Tracks how many times a random monkey was requested.
    private static int _randomPickCount;

    /// <summary>
    /// Gets all monkeys. By default this will use a cached value and refresh the cache
    /// if it's older than the provided timespan or when <paramref name="forceRefresh"/> is true.
    /// </summary>
    /// <param name="sourceUrl">Optional URL to override the MCP data source.</param>
    /// <param name="forceRefresh">If true, forces a refresh from the source.</param>
    /// <param name="cacheTtl">Time-to-live for the cache. Default 10 minutes.</param>
    public static async Task<IEnumerable<Monkey>> GetMonkeysAsync(string? sourceUrl = null, bool forceRefresh = false, TimeSpan? cacheTtl = null)
    {
        cacheTtl ??= TimeSpan.FromMinutes(10);
        sourceUrl ??= DefaultMonkeysUrl;

        if (!forceRefresh && _cache.Count > 0 && (DateTime.UtcNow - _lastFetch) < cacheTtl)
        {
            return _cache;
        }

        await _fetchLock.WaitAsync();
        try
        {
            // Double-check after acquiring the lock
            if (!forceRefresh && _cache.Count > 0 && (DateTime.UtcNow - _lastFetch) < cacheTtl)
            {
                return _cache;
            }

            using var stream = await _httpClient.GetStreamAsync(sourceUrl);
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNameCaseInsensitive = true
            };

            var monkeys = await JsonSerializer.DeserializeAsync<List<Monkey>>(stream, options) ?? new List<Monkey>();

            // Replace cache atomically
            Interlocked.Exchange(ref _cache, monkeys);
            _lastFetch = DateTime.UtcNow;

            return _cache;
        }
        finally
        {
            _fetchLock.Release();
        }
    }

    /// <summary>
    /// Finds a monkey by name (case-insensitive). Returns null if not found.
    /// </summary>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name, string? sourceUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var monkeys = await GetMonkeysAsync(sourceUrl);
        return monkeys.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Returns a random monkey from the collection and increments the random pick counter.
    /// </summary>
    public static async Task<Monkey?> GetRandomMonkeyAsync(string? sourceUrl = null)
    {
        var monkeys = (await GetMonkeysAsync(sourceUrl)).ToList();
        if (monkeys.Count == 0)
            return null;

        var rng = new Random();
        var picked = monkeys[rng.Next(monkeys.Count)];
        Interlocked.Increment(ref _randomPickCount);
        return picked;
    }

    /// <summary>
    /// Gets how many times GetRandomMonkeyAsync has been called successfully.
    /// </summary>
    public static int RandomPickCount => _randomPickCount;

    /// <summary>
    /// Clears the local cache. Useful for testing or forcing a fresh fetch on next call.
    /// </summary>
    public static void ClearCache()
    {
        Interlocked.Exchange(ref _cache, new List<Monkey>());
        _lastFetch = DateTime.MinValue;
    }
}
