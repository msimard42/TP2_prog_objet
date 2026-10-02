namespace TP2Dispatch
{
	public class Event
	{
		const int EVENT_STATS_POINTS = 2;
		const int MAX_STATS_PROBABILITIES = 1;

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
				this._difficulty = value >= Dispatcher.BASE_STARTING_DIFFICULTY ? value : Dispatcher.BASE_STARTING_DIFFICULTY;
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
			float prob = 0.00f;
			
			foreach (Hero hero in heroes)
				for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
					heroesStats[i] += hero.PlayerStats.GetStatValue(Enum.GetValues<StatsName>()[i]);

			for (int i = 0; i < heroesStats.Length; i++)
			{
				float tmpProb = (float)heroesStats[i] / this.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);
				prob += tmpProb > MAX_STATS_PROBABILITIES ? MAX_STATS_PROBABILITIES : tmpProb;
			}

			return prob / Enum.GetValues<StatsName>().Length;
		}

		public EventOutcome ResolveEvent(List<Hero> heroes)
		{
			EventOutcome outcome = EventOutcome.Failure;

			if (heroes is null || heroes.Count == 0)
				return outcome;

			if (RandomGenerator.NextFloat() <= CalculateSuccessProbability(heroes))
				outcome = EventOutcome.Success;

			foreach (Hero hero in heroes)
				hero.ResolveEvent(this, outcome);

			return outcome;
		}

		private Stats CreateStatisticsBasedOnDifficulty()
		{
			int[] stats = new int[Enum.GetValues<StatsName>().Length];
			int pointsLeft = this.Difficulty + EVENT_STATS_POINTS;

			for (int i = 0; i < stats.Length; i++)
				stats[i] = Stats.BASE_STAT_VALUE;

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
