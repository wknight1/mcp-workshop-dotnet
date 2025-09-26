using MyMonkeyApp;

// Display welcome ASCII art
DisplayRandomAsciiArt();

bool exit = false;
while (!exit)
{
    Console.WriteLine("\n🐵 === MONKEY CONSOLE APPLICATION === 🐵");
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. List all monkeys");
    Console.WriteLine("2. Get details for a specific monkey by name");
    Console.WriteLine("3. Get a random monkey");
    Console.WriteLine("4. Exit app");
    Console.Write("\nEnter your choice (1-4): ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ListAllMonkeys();
            break;
        case "2":
            GetMonkeyByName();
            break;
        case "3":
            GetRandomMonkey();
            break;
        case "4":
            exit = true;
            Console.WriteLine("\n👋 Thanks for using the Monkey App! Goodbye!");
            break;
        default:
            Console.WriteLine("\n❌ Invalid choice. Please enter 1, 2, 3, or 4.");
            break;
    }
    
    if (!exit)
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

static void DisplayRandomAsciiArt()
{
    string[] asciiArt = {
        @"
      🐵 MONKEY APP 🐵
         .-""--.
        /       \
       |  O   O  |
       |    >    |
       |   ___   |
        \       /
         '-----'
",
        @"
    🍌 BANANA LOVERS 🍌
           .-""-.
          /     \
         | () () |
          \  ^  /
           ||||
           ||||
",
        @"
      🐒 MONKEY TIME 🐒
        .=""=.
       /     \
      | (o)(o) |
       \   <  /
        ) . (
       /     \
",
        @"
     🙈🙉🙊 WISE MONKEYS
        .-""--.
       /  ><  \    See no evil
      |   --   |   Hear no evil  
       \  __  /    Speak no evil
        '----'
"
    };

    Random random = new();
    int index = random.Next(asciiArt.Length);
    Console.WriteLine(asciiArt[index]);
}

static void ListAllMonkeys()
{
    Console.WriteLine("\n🐵 === ALL AVAILABLE MONKEYS === 🐵\n");
    
    var monkeys = MonkeyHelper.GetAllMonkeys();
    
    foreach (var monkey in monkeys)
    {
        Console.WriteLine($"🐒 Name: {monkey.Name}");
        Console.WriteLine($"   Location: {monkey.Location}");
        Console.WriteLine($"   Population: {monkey.Population:N0}");
        Console.WriteLine($"   Details: {monkey.Details}");
        Console.WriteLine();
    }
    
    Console.WriteLine($"Total monkeys available: {monkeys.Count}");
}

static void GetMonkeyByName()
{
    Console.Write("\n🔍 Enter the monkey name to search for: ");
    string? name = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("❌ Please enter a valid monkey name.");
        return;
    }
    
    var monkey = MonkeyHelper.GetMonkeyByName(name);
    
    if (monkey != null)
    {
        Console.WriteLine($"\n✅ Found monkey: {monkey.Name}");
        Console.WriteLine($"🌍 Location: {monkey.Location}");
        Console.WriteLine($"👥 Population: {monkey.Population:N0}");
        Console.WriteLine($"📝 Details: {monkey.Details}");
    }
    else
    {
        Console.WriteLine($"\n❌ No monkey found with the name '{name}'.");
        Console.WriteLine("💡 Try one of these available monkeys:");
        
        var allMonkeys = MonkeyHelper.GetAllMonkeys();
        foreach (var m in allMonkeys)
        {
            Console.WriteLine($"   - {m.Name}");
        }
    }
}

static void GetRandomMonkey()
{
    var randomMonkey = MonkeyHelper.GetRandomMonkey();
    int accessCount = MonkeyHelper.GetRandomAccessCount();
    
    Console.WriteLine("\n🎲 === RANDOM MONKEY SELECTION === 🎲");
    Console.WriteLine($"🐒 Selected: {randomMonkey.Name}");
    Console.WriteLine($"🌍 Location: {randomMonkey.Location}");
    Console.WriteLine($"👥 Population: {randomMonkey.Population:N0}");
    Console.WriteLine($"📝 Details: {randomMonkey.Details}");
    Console.WriteLine($"\n📊 Random selections made: {accessCount}");
    
    // Display fun random ASCII art for the selected monkey
    string[] randomArt = {
        "🐵🍌", "🐒🌴", "🙈🙉🙊", "🐵💨", "🍌🐒"
    };
    
    Random random = new();
    string art = randomArt[random.Next(randomArt.Length)];
    Console.WriteLine($"\n{art} Lucky monkey selected! {art}");
}
