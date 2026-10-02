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
			int cumulativeSuccess = BASE_STARTING_SUCCESS;
			while (this.NbFailure < 3)
			{
				List<Hero> heroesDispatched = new();
				Console.Clear();
				Console.WriteLine("Nombre de succès : {0, -3} , Nombre d'échecs : {1}\n", this.NbSuccess, this.NbFailure);
				Event currentEvent = new(RandomGenerator.GetRandomEventName(), this.GlobalDifficulty);
				Console.Write(currentEvent);
				for (int i = 0; i < this.HeroesAvailable.Length; i++)
					Program.WriteMessage($"{i + 1}. {this.HeroesAvailable[i]}", this.HeroesAvailable[i].IsResting() ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen);

				int result = -1;
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
					string userInput = Console.ReadLine();
					if (IsValidNumber(userInput, out result) && result > 0 && result <= this.HeroesAvailable.Length)
					{
						Hero selectedHero = HeroesAvailable[result - 1];
						if (selectedHero.IsResting())
							Program.WriteMessage("Hero is resting", ConsoleColor.DarkRed);
						else if (heroesDispatched.Contains(selectedHero))
							Program.WriteMessage("Hero is already dispatched", ConsoleColor.DarkRed);
						else
							heroesDispatched.Add(selectedHero);
					}
					else if (result < 0 || result > this.HeroesAvailable.Length)
					{
						Program.WriteMessage("Choosen number is not valid", ConsoleColor.DarkRed);
					}
				} while (result != 0);
				foreach (Hero hero in this.HeroesAvailable)
					hero.Rest();
				if (currentEvent.ResolveEvent(heroesDispatched) == EventOutcome.Success)
				{
					Program.WriteMessage("Situation is under control", ConsoleColor.DarkGreen);
					this.NbSuccess++;
					if (cumulativeSuccess > 0)
					{
						cumulativeSuccess = 0;
						this.GlobalDifficulty++;
					}
					else
						cumulativeSuccess++;
				}
				else
				{
					this.NbFailure++;
					Program.WriteMessage("Situation is out of control", ConsoleColor.DarkRed);
				}
				Console.ReadKey();
			}
			Console.WriteLine("Votre quart de travail est terminé! Voici la performance de vos héros :\n");
			//foreach (Hero hero in this.HeroesAvailable)
			//	Console.WriteLine($"{hero.Name} : {'{'} {hero.GetHistory()}{'}'}");
		}

		private static bool IsValidNumber(string toCheck, out int number)
		{
			if (!int.TryParse(toCheck, out number) || !IsNumber(toCheck))
			{
				if (!string.IsNullOrEmpty(toCheck))
					number = -1;
				return false;
			}
			return true;
		}

		public static bool IsNumber(string number)
		{
			for (int i = 0; i < number.Length; i++)
			{
				if (number[i] < 48 || number[i] > 57)
				{
					return false;
				}
			}
			return true;
		}
	}
}
