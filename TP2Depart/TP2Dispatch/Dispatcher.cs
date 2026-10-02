namespace TP2Dispatch
{
	public class Dispatcher
	{
		public const int BASE_STARTING_DIFFICULTY = 1;
		const int BASE_STARTING_SUCCESS = 0;
		const int BASE_STARTING_FAILURE = 0;
		const int MAX_NB_OF_FAILURE = 3;
		const int CHAR_ZERO = 48;
		const int CHAR_NINE = 57;

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

			while (this.NbFailure < MAX_NB_OF_FAILURE)
			{
				List<Hero> heroesDispatched = new();

				Console.Clear();
				Console.WriteLine("Nombre de succès : {0, -3} , Nombre d'échecs : {1}\n", this.NbSuccess, this.NbFailure);

				Event currentEvent = new(RandomGenerator.GetRandomEventName(), this.GlobalDifficulty);

				Console.Write(currentEvent);
				for (int i = 0; i < this.HeroesAvailable.Length; i++)
					WriteMessage($"{i + 1}. {this.HeroesAvailable[i]}", this.HeroesAvailable[i].IsResting() ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen);

				int result = -1;
				do
				{
					PrintHeroesDispatched(currentEvent, heroesDispatched);

					result = ReadInput(heroesDispatched, result);

				} while (result != 0);
				foreach (Hero hero in this.HeroesAvailable)
					hero.Rest();
				cumulativeSuccess = PrintEventOutcome(currentEvent, heroesDispatched, cumulativeSuccess);
				Console.WriteLine("Appuyez sur une touche pour confirmer et passer à l'événement suivant...");
				Console.ReadKey();
			}
			PrintHeroesLogs();
		}

		private void PrintHeroesDispatched(Event currentEvent, List<Hero> heroesDispatched)
		{
			Console.Write("Héros envoyés sur la scène : [");
			for (int i = 0; i < heroesDispatched.Count; i++)
			{
				if (i != 0)
					Console.Write(", ");
				Console.Write(heroesDispatched[i].Name);
			}
			Console.WriteLine("]\nChance de réussite actuelle : {0:F2} %", currentEvent.CalculateSuccessProbability(heroesDispatched) * 100);
		}

		private int ReadInput(List<Hero> heroesDispatched, int result)
		{
			string userInput = Console.ReadLine();
			if (IsValidNumber(userInput, out result) && result > 0 && result <= this.HeroesAvailable.Length)
			{
				Hero selectedHero = HeroesAvailable[result - 1];
				if (selectedHero.IsResting())
					WriteMessage("Le héro doit se reposer!", ConsoleColor.DarkRed);
				else if (heroesDispatched.Contains(selectedHero))
					WriteMessage("Le héro est déjà déployé!", ConsoleColor.DarkRed);
				else
					heroesDispatched.Add(selectedHero);
			}
			else if (result < 0 || result > this.HeroesAvailable.Length)
				WriteMessage("La valeure entrée n'est pas valide", ConsoleColor.DarkRed);
			return result;
		}

		private int PrintEventOutcome(Event currentEvent, List<Hero> heroesDispatched, int cumulativeSuccess)
		{
			if (currentEvent.ResolveEvent(heroesDispatched) == EventOutcome.Success)
			{
				WriteMessage("La situation est sous contrôle!", ConsoleColor.DarkGreen);
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
				WriteMessage("La situation est hors de contrôle!", ConsoleColor.DarkRed);
			}
			return cumulativeSuccess;
		}

		private void PrintHeroesLogs()
		{
			Console.WriteLine("\n\nVotre quart de travail est terminé! Voici la performance de vos héros :\n");
			foreach (Hero hero in this.HeroesAvailable)
			{
				bool hasResolveEvents = false;

				Console.Write(hero.Name + " : { ");
				foreach (KeyValuePair<Event, EventOutcome> history in hero.History)
				{
					hasResolveEvents = true;
					Console.Write(history.Key.Name + " : ");
					WriteMessage(history.Value.ToString(), history.Value == EventOutcome.Failure ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen, false);
					Console.Write("; ");
				}
				if (!hasResolveEvents)
					WriteMessage("aucun événement résolu ", ConsoleColor.DarkRed, false);
				Console.WriteLine("}\n");
			}
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

		private static bool IsNumber(string number)
		{
			for (int i = 0; i < number.Length; i++)
			{
				if (number[i] < CHAR_ZERO || number[i] > CHAR_NINE)
					return false;
			}
			return true;
		}

		private static void WriteMessage(string message, ConsoleColor color, bool newLine = true)
		{
			Console.ForegroundColor = color;
			if (newLine)
				Console.WriteLine(message);
			else
				Console.Write(message);
			Console.ResetColor();
		}
	}
}
