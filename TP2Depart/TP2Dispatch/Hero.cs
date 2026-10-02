using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TP2Dispatch
{
	public class Hero
	{
		const int STARTING_LEVEL = 1;
		const int STAT_GAIN_PER_LEVEL = 1;
		const int STARTING_REST_AMOUNT = 0;
		const int NOT_RESTING_VALUE = 0;
		const int MISSION_REST_INCREASE = 2;
		const int FAILURE_REST_INCREASE = 1;
		const int MAXIMUM_STAT = 10;

		private string _name;
		private int _level;
		private int _restRemaining;
		private Stats _playerStats;
		private Dictionary<Event, EventOutcome> _history;

		public string Name
		{
			get => _name;
			private set
			{
				_name = value;
			}
		}

		public int Level
		{
			get => _level;
			private set
			{
				_level = value;
			}
		}

		public int RestRemaining
		{
			get => _restRemaining;
			private set
			{
				if (value < 0)
					_restRemaining = 0;
				else
					_restRemaining = value;
			}
		}

		public Stats PlayerStats
		{
			get => _playerStats;
			private set
			{
				_playerStats = value;
			}
		}

		public Dictionary<Event, EventOutcome> History
		{
			get => _history;
			private set
			{
				_history = value;
			}
		}

		public Hero(string name, Stats stats)
		{
			this.Name = name;
			this.Level = STARTING_LEVEL;
			this.RestRemaining = STARTING_REST_AMOUNT;
			stats.LimitStatMax(MAXIMUM_STAT);
			this.PlayerStats = stats;
			this.History = new Dictionary<Event, EventOutcome>();
		}

		public bool IsResting()
		{
			bool isResting = false;
			if (this.RestRemaining > NOT_RESTING_VALUE)
				isResting = true;
			return isResting;
		}

		public void Rest()
		{
			this.RestRemaining--;
		}

		public void ResolveEvent(Event eventDone, EventOutcome outcome)
		{
			int restNeeded = MISSION_REST_INCREASE;
			if (outcome == EventOutcome.Success)
				this.LevelUp();
			else
				restNeeded += FAILURE_REST_INCREASE;
			this.RestRemaining += restNeeded;
			this.History.Add(eventDone, outcome);
		}

		private void LevelUp()
		{
			Random rng = new Random();
			this.Level++;
			int amountToSplit = STAT_GAIN_PER_LEVEL;
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			bool canIncreaseStat = true;
			do
			{
				if (this.HasMaxStats())
					canIncreaseStat = false;
				else
				{
					int choosenIncrease = rng.Next(0, nbOfStats);
					StatsName choosenStat = (StatsName)choosenIncrease;
					int choosenStatCurrentValue = this.PlayerStats.GetStatValue(choosenStat);
					if (choosenStatCurrentValue + amountToSplit <= MAXIMUM_STAT)
					{
						this.PlayerStats.IncreaseStat(choosenStat, amountToSplit);
						amountToSplit = 0;
					}
					else
					{
						int allowedIncrease = MAXIMUM_STAT - choosenStatCurrentValue;
						this.PlayerStats.IncreaseStat(choosenStat, allowedIncrease);
						amountToSplit -= allowedIncrease;
					}
					if (amountToSplit == 0)
						canIncreaseStat = false;
				}
			} while (canIncreaseStat);
		}

		private bool HasMaxStats()
		{
			bool hasMaxStats = true;
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			bool[] statIsMaxed = new bool[nbOfStats];
			for (int i = 0; i < nbOfStats; i++)
			{
				if (PlayerStats.GetStatValue((StatsName)i) >= MAXIMUM_STAT)
					statIsMaxed[i] = true;
				else
					statIsMaxed[i] = false;
			}
			for (int i = 0; i < statIsMaxed.Length; i++)
			{
				if (statIsMaxed[i] == false)
					hasMaxStats = false;
			}
			return hasMaxStats;
		}

		public override string ToString()
		{
			return $"[{this.RestRemaining}] {this.Name, -24} , Level {this.Level} : {this.PlayerStats}";
		}
	}
}
