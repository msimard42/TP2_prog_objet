using System;

namespace TP2Dispatch
{
	public class Program
	{

		public static void Main(string[] args)
		{
		}

		public static void TestHeroClass()
		{
			Stats stats = new Stats();
			StatsName statName = StatsName.Intelligence;
			Hero flash = new Hero("Flash", stats);
			try
			{
				int vigorValue = stats.GetStatValue(statName);
				Console.WriteLine($"{flash.Name} has the following stats: {flash.PlayerStats}");
				int levelToGain = 40;
				for (int i = 0; i < levelToGain; i++)
				{
					flash.Levelup();
				}
				Console.WriteLine($"{flash.Name}'s stats after the {levelToGain} levelup: {flash.PlayerStats}");
			}
			catch (ArgumentOutOfRangeException err)
			{
				Console.WriteLine(err);
			}
		}
	}
}