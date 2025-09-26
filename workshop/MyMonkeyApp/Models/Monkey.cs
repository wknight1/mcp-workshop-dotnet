using System;
using System.Text.Json.Serialization;

namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a monkey species or character used by the MyMonkeyApp console application.
/// Maps closely to the MCP response for easy deserialization.
/// </summary>
public sealed class Monkey
{
    /// <summary>
    /// Unique identifier for local usage. Generated when not provided.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Common name of the monkey (e.g. "Baboon").
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Human-readable location (e.g. "Africa & Asia").
    /// </summary>
    public string Location { get; init; } = string.Empty;

    /// <summary>
    /// Descriptive details about the monkey.
    /// </summary>
    public string? Details { get; init; }

    /// <summary>
    /// URL to an image representing the monkey. This maps to the MCP JSON property named "Image".
    /// </summary>
    [JsonPropertyName("Image")]
    public string? ImageUrl { get; init; }

    /// <summary>
    /// Approximate population count, when available.
    /// </summary>
    public int Population { get; init; }

    /// <summary>
    /// Representative latitude for the monkey's location, if known.
    /// </summary>
    public double? Latitude { get; init; }

    /// <summary>
    /// Representative longitude for the monkey's location, if known.
    /// </summary>
    public double? Longitude { get; init; }

    /// <summary>
    /// Short display string used by the console UI.
    /// </summary>
    public string DisplayText => string.IsNullOrWhiteSpace(Location)
        ? $"{Name} (Pop: {Population})"
        : $"{Name} — {Location} (Pop: {Population})";

    /// <summary>
    /// Performs simple validation for required fields and value ranges.
    /// </summary>
    /// <param name="validationMessage">An error message when validation fails.</param>
    /// <returns>True if valid; otherwise false.</returns>
    public bool IsValid(out string? validationMessage)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            validationMessage = "Name is required.";
            return false;
        }

        if (Population < 0)
        {
            validationMessage = "Population cannot be negative.";
            return false;
        }

        validationMessage = null;
        return true;
    }
}
