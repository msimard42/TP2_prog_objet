using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TP2Dispatch
{
	public class Hero
	{
		private string _name;
		private int _level;
		private int _restRemaining;
		private Stats _playerStats;
		private Dictionary<Event, bool> _history;

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

		public Dictionary<Event, bool> History
		{
			get => _history;
			private set
			{
				_history = value;
			}
		}

		//public Heroes(string name, 
	}
}
