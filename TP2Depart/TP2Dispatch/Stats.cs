using System;
using System.Collections.Generic;
using System.Text;

namespace TP2Dispatch
{
	public class Stats
	{
		const int BASE_STAT_VALUE = 1;
		//private int _mobility;
		//private int _vigor;
		//private int _intelligence;
		//private int _charisma;
		private Dictionary<StatsName, int> _statsList;

		//public int Mobility
		//{
		//	get => _mobility;
		//	private set
		//	{
		//		if (value < 1)
		//			_mobility = BASE_STAT_VALUE;
		//		else
		//			_mobility = value;
		//	}
		//}

		//public int Vigor
		//{
		//	get => _vigor;
		//	private set
		//	{
		//		if (value < 1)
		//			_vigor = BASE_STAT_VALUE;
		//		else
		//			_vigor = value;
		//	}
		//}

		//public int Intelligence
		//{
		//	get => _intelligence;
		//	private set
		//	{
		//		if (value < 1)
		//			_intelligence = BASE_STAT_VALUE;
		//		else
		//			_intelligence = value;
		//	}
		//}

		//public int Charisma
		//{
		//	get => _charisma;
		//	private set
		//	{
		//		if (value < 1)
		//			_charisma = BASE_STAT_VALUE;
		//		else
		//			_charisma = value;
		//	}
		//}

		public Dictionary<StatsName, int> StatsList
		{
			get => _statsList;
			private set
			{
				_statsList = value;
			}
		}

		public Stats()
		{
			//Mobility = BASE_STAT_VALUE;
			//Vigor = BASE_STAT_VALUE;
			//Intelligence = BASE_STAT_VALUE;
			//Charisma = BASE_STAT_VALUE;
			StatsList = new Dictionary<StatsName, int>();
			int nbOfStats = Enum.GetValues<StatsName>().Length;
			for (int i = 0; i < nbOfStats; i++)
			{
				StatsName statName = ((StatsName)i);
				StatsList.Add(statName, BASE_STAT_VALUE);
			}
		}

		public Stats(int mobility, int vigor, int intelligence, int charisma)
		{
			this.StatsList = new Dictionary<StatsName, int>();
			this.StatsList.Add(StatsName.Mobility, mobility);
			this.StatsList.Add(StatsName.Vigor, vigor);
			this.StatsList.Add(StatsName.Intelligence, intelligence);
			this.StatsList.Add(StatsName.Charisma, charisma);
		}

		public void IncreaseRandomStats(int amountToSplit)
		{
			//to do
		}

		public int GetStatValue(StatsName name)
		{
			int statValue = -1;
			foreach (KeyValuePair<StatsName, int> stat in StatsList)
			{
				if (name == stat.Key)
					statValue = stat.Value;
			}
			if (statValue < 0)
			{
				throw new ArgumentOutOfRangeException("The searched Enum was out of bound");
			}
			return statValue;
		}
	}
}
