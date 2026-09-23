using System;
using System.Collections.Generic;
using System.Text;

namespace TP2Dispatch
{
	public class Stats
	{
		const int BASE_STAT_VALUE = 1;
		private string[] _statsName;
		private int _mobility;
		private int _vigor;
		private int _intelligence;
		private int _charisma;
		private Dictionary<string, int> _statsList;

		public string[] StatsName
		{
			get => _statsName;
			private set
			{
				_statsName = value;
			}
		}

		public int Mobility
		{
			get => _mobility;
			private set
			{
				if (value < 1)
					_mobility = BASE_STAT_VALUE;
				else
					_mobility = value;
			}
		}

		public int Vigor
		{
			get => _vigor;
			private set
			{
				if (value < 1)
					_vigor = BASE_STAT_VALUE;
				else
					_vigor = value;
			}
		}

		public int Intelligence
		{
			get => _intelligence;
			private set
			{
				if (value < 1)
					_intelligence = BASE_STAT_VALUE;
				else
					_intelligence = value;
			}
		}

		public int Charisma
		{
			get => _charisma;
			private set
			{
				if (value < 1)
					_charisma = BASE_STAT_VALUE;
				else
					_charisma = value;
			}
		}

		public Dictionary<string, int> StatsList
		{
			get => _statsList;
			private set
			{
				_statsList = value;
			}
		}

		public Stats()
		{
			StatsName = [ "Mobility", "Vigor", "Intelligence", "Charisma" ];
			Mobility = BASE_STAT_VALUE;
			Vigor = BASE_STAT_VALUE;
			Intelligence = BASE_STAT_VALUE;
			Charisma = BASE_STAT_VALUE;
			StatsList = new Dictionary<string, int>();
			for (int i = 0; i < StatsList.Count; i++)
			{
				StatsList[StatsName[i]] = BASE_STAT_VALUE;
			}
		}

		public Stats(int mobility, int vigor, int intelligence, int charisma)
		{
			this.Mobility = mobility;
			this.StatsList[nameof(mobility)] = mobility;
			this.Vigor = vigor;
			this.StatsList[nameof(vigor)] = vigor;
			this.Intelligence = intelligence;
			this.StatsList[nameof(intelligence)] = intelligence;
			this.Charisma = charisma;
			this.StatsList[nameof(charisma)] = charisma;
		}

		public void IncreaseRandomStats(int amountToSplit)
		{
			//to do
		}
	}
}
