namespace TP2Dispatch
{
	public class Dispatcher
	{
		const int BASE_STARTING_SUCCESS = 0;
		const int BASE_STARTING_FAILURE = 0;
		const int BASE_STARTING_DIFFICULTY = 1;
		private int _nbSuccess;
		private int _nbFailure;
		private Hero[] _heroesAvailable;
		private int _globalDifficulty;

		public int NbSuccess
		{
			get => _nbSuccess;
			private set
			{
				_nbSuccess = value;
			}
		}

		public int NbFailure
		{
			get => _nbFailure;
			private set
			{
				_nbFailure = value;
			}
		}

		public Hero[] HeroesAvailable
		{
			get => _heroesAvailable;
			private set
			{
				_heroesAvailable = value;
			}
		}

		public int GlobalDifficulty
		{
			get => _globalDifficulty;
			private set
			{
				_globalDifficulty = value;
			}
		}

		public Dispatcher()
		{
			this.NbSuccess = BASE_STARTING_SUCCESS;
			this.NbFailure = BASE_STARTING_FAILURE;
			this.HeroesAvailable = [
				new("Ultra Raph", new(1, 3, 2, 2)),
				new("Supermmanuel", new(3, 1, 2, 2)),
				new("PF CaméLéon", new(1, 2, 4, 1)),
				new("Aliday", new(3, 2, 2, 1)),
				new("GP le PC", new(2, 2, 2, 2))
			];
			this.GlobalDifficulty = BASE_STARTING_DIFFICULTY;
		}

		public void DispatchHeroes()
		{
			List<Hero> heroesDispatched = new();
			while (this.NbFailure < 3)
			{
				Console.Clear();
				Console.WriteLine("Nombre de succès : {0, -3} , Nombre d'échecs : {1}\n", this.NbSuccess, this.NbFailure);
				Event currentEvent = new(RandomGenerator.GetRandomEventName(), this.GlobalDifficulty);
				Console.Write(currentEvent);
				for (int i = 0; i < this.HeroesAvailable.Length; i++)
				{
					Console.ForegroundColor = this.HeroesAvailable[i].IsResting() ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
					Console.WriteLine("{0}. [{1}] {2, -24} , Level {3} : {4}", i + 1, this.HeroesAvailable[i].RestRemaining, this.HeroesAvailable[i].Name, this.HeroesAvailable[i].Level, this.HeroesAvailable[i].PlayerStats);
				}
				Console.ResetColor();

				do
				{
					Console.Write("Héros envoyés sur la scène : [");
					for (int i = 0; i < heroesDispatched.Count; i++)
					{
						if (i != 0)
							Console.Write(", ");
						Console.Write(heroesDispatched[i].Name);
					}
					Console.WriteLine("]\nChance de réussite actuelle : {0:F2} %", currentEvent.CalculateSuccessProbability(heroesDispatched) * 100);
				} while (int.TryParse(Console.ReadLine(), out int result) && result != 0); //tmp
				Console.WriteLine("Outcome " + currentEvent.ResolveEvent(heroesDispatched));
				Console.ReadLine();
			}
		}
	}
}
