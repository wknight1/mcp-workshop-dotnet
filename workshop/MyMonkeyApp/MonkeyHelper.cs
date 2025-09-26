namespace MyMonkeyApp;

public static class MonkeyHelper
{
    private static int _randomAccessCount = 0;
    
    private static readonly List<Monkey> _monkeys = new()
    {
        new Monkey
        {
            Name = "Baboon",
            Location = "Africa & Asia",
            Population = 200000,
            Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio.",
            ImageUrl = "https://example.com/baboon.jpg"
        },
        new Monkey
        {
            Name = "Capuchin Monkey",
            Location = "Central & South America",
            Population = 100000,
            Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae.",
            ImageUrl = "https://example.com/capuchin.jpg"
        },
        new Monkey
        {
            Name = "Blue Monkey",
            Location = "East Africa",
            Population = 50000,
            Details = "The blue monkey is a species of Old World monkey native to East and Central Africa.",
            ImageUrl = "https://example.com/bluemonkey.jpg"
        },
        new Monkey
        {
            Name = "Squirrel Monkey",
            Location = "Central & South America",
            Population = 150000,
            Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri.",
            ImageUrl = "https://example.com/squirrelmonkey.jpg"
        },
        new Monkey
        {
            Name = "Golden Lion Tamarin",
            Location = "Brazil",
            Population = 3000,
            Details = "The golden lion tamarin is a small New World monkey of the family Callitrichidae.",
            ImageUrl = "https://example.com/goldentamarin.jpg"
        },
        new Monkey
        {
            Name = "Howler Monkey",
            Location = "Central & South America",
            Population = 80000,
            Details = "Howler monkeys are the loudest of all New World monkeys.",
            ImageUrl = "https://example.com/howlermonkey.jpg"
        },
        new Monkey
        {
            Name = "Japanese Macaque",
            Location = "Japan",
            Population = 120000,
            Details = "The Japanese macaque, also known as the snow monkey, is a terrestrial Old World monkey.",
            ImageUrl = "https://example.com/japanesemacaque.jpg"
        }
    };

    public static List<Monkey> GetAllMonkeys()
    {
        return _monkeys.ToList();
    }

    public static Monkey? GetMonkeyByName(string name)
    {
        return _monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public static Monkey GetRandomMonkey()
    {
        _randomAccessCount++;
        var random = new Random();
        var index = random.Next(_monkeys.Count);
        return _monkeys[index];
    }

    public static int GetRandomAccessCount()
    {
        return _randomAccessCount;
    }
}