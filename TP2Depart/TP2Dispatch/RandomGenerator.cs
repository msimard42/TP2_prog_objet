namespace TP2
{
	public static class RandomGenerator
	{
		private static Random random = new Random();

		private static string[] eventActions = new string[]
		{
			"Secourir", 
			"Protéger",
			"Escorter", 
			"Neutraliser",
			"Évacuer",
			"Retrouver",
			"Libérer",
			"Capturer"
		};

		private static string[] eventActors = new string[]
		{
			"Grand-Mère",
			"les étudiants perdus",
			"le maire",
			"l'enfant hors de contrôle",
			"le passager du tramway",
			"Poufi le chien",
			"la petite famille",
			"tante Yvette"
		};

		public static int Next(int minValue = 0, int maxValue = int.MaxValue)
		{
			return random.Next(minValue, maxValue);
		}

		public static float NextFloat()
		{
			return random.NextSingle();
		}

		public static string GetRandomEventName()
		{
			return $"{eventActions[Next(0, eventActions.Length)]} {eventActors[Next(0, eventActors.Length)]}";
		}
	}
}
