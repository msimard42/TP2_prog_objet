using System;

namespace TP2Dispatch
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Dispatcher dispatch = new();
			dispatch.DispatchHeroes();
		}

		public static void WriteMessage(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}