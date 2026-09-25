namespace TP2Dispatch
{
	public class Event
	{
		const int POINTS = 2; //tmp name
		const int BASE_STAT = 1;
		private string _name;
		private int _difficulty;
		private Stats _stat;

		public string Name
		{
			get { return this._name; }
			private set
			{
				this._name = value;
			}
		}

		public int Difficulty
		{
			get { return this._difficulty; }
			private set
			{
				this._difficulty = value > 0 ? value : 1;
			}
		}

		public Stats Stat
		{
			get { return this._stat; }
			private set
			{
				this._stat = value;
			}
		}

		public Event(string name, int difficulty)
		{
			this.Name = name;
			this.Difficulty = difficulty;
			this.Stat = CreateStatisticsBasedOnDifficulty();
		}

		public float CalculateSuccessProbability(List<Hero> heroes)
		{
			int[] heroesStats = new int[Enum.GetValues<StatsName>().Length];
			float prob = 0.00f; // tmp name
			
			foreach (Hero hero in heroes)
				for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
					heroesStats[i] += hero.PlayerStats.GetStatValue(Enum.GetValues<StatsName>()[i]);

			for (int i = 0; i < heroesStats.Length; i++)
			{
				float tmp = (float)heroesStats[i] / this.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]); //tmp name
				prob += tmp > 1 ? 1 : tmp;
			}

			return prob / Enum.GetValues<StatsName>().Length;
		}

		public EventOutcome ResolveEvent(List<Hero> heroes)
		{
			EventOutcome outcome = EventOutcome.Echec;

			if (heroes is null || heroes.Count == 0)
				return outcome;

			if (RandomGenerator.NextFloat() < CalculateSuccessProbability(heroes))
				outcome = EventOutcome.Succes;

			foreach (Hero hero in heroes)
				; // hero.ResolveEvent(this.Name, outcome);

			return outcome;
		}

		private Stats CreateStatisticsBasedOnDifficulty()
		{
			int[] stats = new int[Enum.GetValues<StatsName>().Length];
			int pointsLeft = this.Difficulty + POINTS;

			for (int i = 0; i < stats.Length; i++)
				stats[i] = BASE_STAT;

			while (pointsLeft > 0)
			{
				stats[RandomGenerator.Next(0, Enum.GetValues<StatsName>().Length)]++;
				pointsLeft--;
			}

			return new(stats[(int)StatsName.Vigor], stats[(int)StatsName.Mobility], stats[(int)StatsName.Intelligence], stats[(int)StatsName.Charisma]);
		}

		public override string ToString()
		{
			return $"Situation nécessitant une intervention : {this.Name}, Difficulté : {this.Difficulty}\n" +
				$"Attributs nécessaires pour répondre à la situation : {this.Stat}\n";
		}
	}
}
