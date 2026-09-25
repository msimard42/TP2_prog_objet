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
		const int BASE_REST_AMOUNT = 0;
		const int MISSION_REST_INCREASE = 2;
		const int FAILURE_REST_INCREASE = 1;
		const int MAXIMUM_STAT = 10;

		private string _name;
		private int _level;
		private int _restRemaining;
		private Stats _playerStats;
		private Dictionary<string, EventOutcome> _history;

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

		private Dictionary<string, EventOutcome> History
		{
			get => _history;
			set
			{
				_history = value;
			}
		}

		public Hero(string name, Stats stats)
		{
			this.Name = name;
			this.Level = STARTING_LEVEL;
			this.RestRemaining = BASE_REST_AMOUNT;
			this.PlayerStats = stats;
			this.History = new Dictionary<string, EventOutcome>();
		}

		public bool IsResting()
		{
			bool isResting = false;
			if (this.RestRemaining > BASE_REST_AMOUNT)
				isResting = true;
			return isResting;
		}

		public void Rest()
		{
			this.RestRemaining--;
		}

		public void ResolveEvent(string eventDone, EventOutcome outcome)
		{
			int restNeeded = MISSION_REST_INCREASE;
			if (outcome == EventOutcome.Succes)
				this.Levelup();
			else
				restNeeded += FAILURE_REST_INCREASE;
			this.History.Add(eventDone, outcome);
		}

		public void Levelup()
		{
			Random rng = new Random();
			this.Level++;
			int amountToSplit = STAT_GAIN_PER_LEVEL;
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			bool canIncreaseStat = true;
			do
			{
				if (this.hasMaxStats())
					canIncreaseStat = false;
				else
				{
					int choosenIncrease = rng.Next(0, nbOfStats);
					StatsName choosenStat = (StatsName)choosenIncrease;
					int choosenStatCurrentValue = this.PlayerStats.GetStatValue(choosenStat);
					Console.WriteLine($"{this.Name} has {choosenStatCurrentValue} {choosenStat} and wants to increase it by {amountToSplit}");
					if (choosenStatCurrentValue + amountToSplit <= MAXIMUM_STAT)
					{
						this.PlayerStats.IncreaseStat(choosenStat, amountToSplit);
						amountToSplit = 0;
						Console.WriteLine($"{choosenStat} is now {this.PlayerStats.GetStatValue(choosenStat)}");
					}
					else
					{
						int allowedIncrease = MAXIMUM_STAT - choosenStatCurrentValue;
						this.PlayerStats.IncreaseStat(choosenStat, allowedIncrease);
						amountToSplit -= allowedIncrease;
						Console.WriteLine($"Allowed increase = {allowedIncrease}. Amount to split is now {amountToSplit}");
					}
					if (amountToSplit == 0)
						canIncreaseStat = false;
				}
			} while (canIncreaseStat);
		}

		private bool hasMaxStats()
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
	}
}
