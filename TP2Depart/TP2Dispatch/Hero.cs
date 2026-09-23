using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TP2Dispatch
{
	public class Hero
	{
		const int STARTING_LEVEL = 1;
		const int BASE_REST_AMOUNT = 0;

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

		public Dictionary<string, EventOutcome> History
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
			this.RestRemaining = BASE_REST_AMOUNT;
			this.PlayerStats = stats;
			this.History = new Dictionary<string, EventOutcome>();
		}
	}
}
