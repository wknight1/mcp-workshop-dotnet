using System;
using System.Linq;
using System.Threading.Tasks;
using MyMonkeyApp.Helpers;
using MyMonkeyApp.Models;

namespace MyMonkeyApp;

public static class Program
{
	private static readonly Random _rng = new();

	private static readonly string[] AsciiArts = new[]
	{
		"   .-\"\"\"-.",
		"  /       \\",
		" |  .-. .- |",
		" |  |_| |_| |",
		" |  (o) (o) |",
		" |   \\   /  |",
		"  \\  '---' /",
		"   '-.___.-'",
		"",
		"  (\"\"\"\")  (\"\"\")",
		"  (\"o_o\")  (^-^)",
		"   ( > < )",
		"    (___)"
	};

	public static async Task Main(string[] args)
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;
		Console.WriteLine("Welcome to MyMonkeyApp — Monkey Explorer!");
		MaybeShowAscii(0.6);

		while (true)
		{
			Console.WriteLine();
			Console.WriteLine("Please choose an option:");
			Console.WriteLine("1) List all monkeys");
			Console.WriteLine("2) Get details for a specific monkey by name");
			Console.WriteLine("3) Get a random monkey");
			Console.WriteLine("4) Exit app");
			Console.Write("> ");

			var input = Console.ReadLine()?.Trim();
			Console.WriteLine();

			try
			{
				switch (input)
				{
					case "1":
						await ListAllAsync();
						break;
					case "2":
						await GetByNameAsync();
						break;
					case "3":
						await GetRandomAsync();
						break;
					case "4":
					case "q":
					case "quit":
						Console.WriteLine("Goodbye!");
						return;
					default:
						Console.WriteLine("Invalid selection. Please enter 1-4.");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			MaybeShowAscii(0.3);
		}
	}

	private static void MaybeShowAscii(double chance)
	{
		if (_rng.NextDouble() <= chance)
		{
			var art = string.Join(Environment.NewLine, AsciiArts.OrderBy(_ => _rng.Next()).Take(7));
			Console.WriteLine(art);
		}
	}

	private static async Task ListAllAsync()
	{
		Console.WriteLine("Fetching monkeys...");
		var monkeys = (await MonkeyHelper.GetMonkeysAsync()).ToList();
		if (!monkeys.Any())
		{
			Console.WriteLine("No monkeys available.");
			return;
		}

		Console.WriteLine($"Found {monkeys.Count} monkeys:\n");
		Console.WriteLine($"{"Name",-30} {"Location",-30} {"Population",10}");
		Console.WriteLine(new string('-', 75));

		foreach (var m in monkeys)
		{
			var name = Truncate(m.Name, 30);
			var loc = Truncate(m.Location, 30);
			Console.WriteLine($"{name,-30} {loc,-30} {m.Population,10}");
		}

		Console.WriteLine();
		Console.WriteLine($"(Tip) Ask for details by selecting option 2 and entering a name, e.g. 'Baboon'");
	}

	private static async Task GetByNameAsync()
	{
		Console.Write("Enter monkey name: ");
		var name = Console.ReadLine()?.Trim() ?? string.Empty;
		if (string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("Name cannot be empty.");
			return;
		}

		Console.WriteLine($"Searching for '{name}'...");
		var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name);
		if (monkey is null)
		{
			Console.WriteLine("Monkey not found.");
			return;
		}

		PrintMonkeyDetails(monkey);
	}

	private static async Task GetRandomAsync()
	{
		Console.WriteLine("Picking a random monkey...");
		var monkey = await MonkeyHelper.GetRandomMonkeyAsync();
		if (monkey is null)
		{
			Console.WriteLine("No monkeys available to pick.");
			return;
		}

		PrintMonkeyDetails(monkey);
		Console.WriteLine($"(Random picks so far: {MonkeyHelper.RandomPickCount})");
	}

	private static void PrintMonkeyDetails(Monkey m)
	{
		Console.WriteLine(new string('=', 60));
		Console.WriteLine($"Name: {m.Name}");
		Console.WriteLine($"Location: {m.Location}");
		Console.WriteLine($"Population: {m.Population}");
		if (m.Latitude.HasValue && m.Longitude.HasValue)
		{
			Console.WriteLine($"Coordinates: {m.Latitude.Value}, {m.Longitude.Value}");
		}
		if (!string.IsNullOrWhiteSpace(m.Details))
		{
			Console.WriteLine();
			Console.WriteLine(m.Details);
		}
		if (!string.IsNullOrWhiteSpace(m.ImageUrl))
		{
			Console.WriteLine();
			Console.WriteLine($"Image: {m.ImageUrl}");
		}
		Console.WriteLine(new string('=', 60));
	}

	private static string Truncate(string? value, int maxLength)
	{
		if (string.IsNullOrEmpty(value)) return string.Empty;
		return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
	}
}
