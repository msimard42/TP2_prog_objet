using System;

namespace TP2Dispatch
{
	public class Program
	{

		public static void Main(string[] args)
		{
			Stats stats = new Stats();
			StatsName name = StatsName.Intelligence;
			try
			{
				int vigorValue = stats.GetStatValue(name);
				Console.WriteLine($"Value of {name} = {vigorValue}");
			}
			catch (ArgumentOutOfRangeException err)
			{
				Console.WriteLine(err);
			}
		}
	}
}