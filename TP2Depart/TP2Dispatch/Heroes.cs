using System;
using System.Collections.Generic;
using System.Text;

namespace TP2Dispatch
{
	public class Heroes
	{
		private string _name;
		private int _level;
		private int _restRemaining;
		private Dictionary<Event, bool> _history;
		private Dictionary<string, int> _stats;
	}
}
