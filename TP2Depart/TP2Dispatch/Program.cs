using System;

namespace TP2Dispatch
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Dispatcher dispatch = new();
			dispatch.DispatchHeroes();
		}

		public static void TestHeroClass()
		{
			Stats stats = new Stats();
			StatsName statName = StatsName.Intelligence;
			Hero flash = new Hero("Flash", stats);
			try
			{
				int vigorValue = stats.GetStatValue(statName);
				Console.WriteLine($"{flash.Name} has the following stats: {flash.PlayerStats}");
				int levelToGain = 40;
				for (int i = 0; i < levelToGain; i++)
				{
					flash.Levelup();
				}
				Console.WriteLine($"{flash.Name}'s stats after the {levelToGain} levelup: {flash.PlayerStats}");
			}
			catch (ArgumentOutOfRangeException err)
			{
				Console.WriteLine(err);
			}
		}

		public static void testMainEvent()
		{
			Hero[] heroes = {
				new ("Ultra Raph", new (1, 3, 2, 2)),
				new ("Supermmanuel", new (3, 1, 2, 2)),
				new ("PF CaméLéon", new (1, 2, 4, 1)),
				new ("Aliday", new (3, 2, 2, 1)),
				new ("GP le PC", new (2, 2, 2, 2))
			};

			for (int i = 0; i < 3; i++)
			{
				PrintGame(new(RandomGenerator.GetRandomEventName(), i + 1), heroes);
			}
		}

		public static void PrintGame(Event @event, Hero[] heroes)
		{
			List<Hero> heroesDispatch = new();
			heroesDispatch.Add(heroes[0]);

			Console.Clear();
			Console.WriteLine("Nombre de succès : {0, -3} , Nombre d'échecs : {1}\n", 0, 0);
			Console.Write(@event);

			for (int i = 0; i < heroes.Length; i++)
			{
				Console.ForegroundColor = heroes[i].IsResting() ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
				Console.WriteLine("{0}. [{1}] {2, -24} , Level {3} : {4}", i + 1, heroes[i].RestRemaining, heroes[i].Name, heroes[i].Level, heroes[i].PlayerStats);
			}
			Console.ResetColor();

			do
			{
				Console.Write("Héros envoyés sur la scène : [");
				for (int i = 0; i < heroesDispatch.Count; i++)
				{
					if (i != 0)
						Console.Write(", ");
					Console.Write(heroesDispatch[i].Name);
				}
				Console.WriteLine("]\nChance de réussite actuelle : {0:F2} %", @event.CalculateSuccessProbability(heroesDispatch) * 100);
			} while (int.TryParse(Console.ReadLine(), out int result) && result != 0); //tmp
			Console.WriteLine("Outcome " + @event.ResolveEvent(heroesDispatch));
			Console.ReadLine();
		}
	}
}