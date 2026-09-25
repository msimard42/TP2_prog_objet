using System;
using System.Collections.Generic;
using System.Text;

namespace TP2Dispatch
{
	public class Stats
	{
		const int BASE_STAT_VALUE = 1;

		private Dictionary<StatsName, int> _statsList;

		private Dictionary<StatsName, int> StatsList
		{
			get => _statsList;
			set
			{
				_statsList = value;
			}
		}

		public Stats()
		{
			StatsList = new Dictionary<StatsName, int>();
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			for (int i = 0; i < nbOfStats; i++)
			{
				StatsName statName = ((StatsName)i);
				StatsList.Add(statName, BASE_STAT_VALUE);
			}
		}

		public Stats(int vigor, int mobility, int intelligence, int charisma)
		{
			this.StatsList = new Dictionary<StatsName, int>();
			this.StatsList.Add(StatsName.Vigor, vigor);
			this.StatsList.Add(StatsName.Mobility, mobility);
			this.StatsList.Add(StatsName.Intelligence, intelligence);
			this.StatsList.Add(StatsName.Charisma, charisma);
		}

		public void IncreaseStat(StatsName statToIncrease, int amountToIncrease)
		{
			this.StatsList[statToIncrease] += amountToIncrease;
		}

		public int GetStatValue(StatsName statName)
		{
			int statValue = -1;
			foreach (KeyValuePair<StatsName, int> stat in StatsList)
			{
				if (statName == stat.Key)
					statValue = stat.Value;
			}
			if (statValue < 0)
			{
				throw new ArgumentOutOfRangeException("The searched Enum was out of bound");
			}
			return statValue;
		}

		public override string ToString()
		{
			string statsAsString = string.Empty;
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			for (int i = 0; i < nbOfStats; i++)
			{
				statsAsString += $"{this.StatsList.ElementAt(i).Key} : {this.StatsList[(StatsName)i],-3} ";
			}
			return statsAsString;
		}
	}
}
