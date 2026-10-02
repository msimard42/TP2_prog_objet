using TP2Dispatch;

namespace TestTP2Dispatch{
	public class EventTests
	{
		[Fact]
		public void Constructor_GoodValue_Ok()
		{
			string name = "allo";
			int difficulty = 1;
			int totalStatsPoints = 7;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, difficulty);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(name, @event.Name);
			Assert.Equal(difficulty, @event.Difficulty);
			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}

		[Fact]
		public void Constructor_Difficulty0_DifficultyEquals1()
		{
			string name = "allo";
			int difficulty = 1;
			int totalStatsPoints = 7;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, 0);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(name, @event.Name);
			Assert.Equal(difficulty, @event.Difficulty);
			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}

		[Fact]
		public void Constructor_NegativeDifficulty_DifficultyEquals1()
		{
			string name = "allo";
			int difficulty = 1;
			int totalStatsPoints = 7;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, -1);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(name, @event.Name);
			Assert.Equal(difficulty, @event.Difficulty);
			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}

		[Fact]
		public void CreateStatisticsBasedOnDifficulty_Difficulty1_7()
		{
			string name = "allo";
			int difficulty = 1;
			int totalStatsPoints = 7;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, difficulty);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}

		[Fact]
		public void CreateStatisticsBasedOnDifficulty_Difficulty3_9()
		{
			string name = "allo";
			int difficulty = 3;
			int totalStatsPoints = 9;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, difficulty);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}

		[Fact]
		public void CreateStatisticsBasedOnDifficulty_Difficulty2147483641_2147483647()
		{
			string name = "allo";
			int difficulty = 2147483641;
			int totalStatsPoints = 2147483647;
			int actualTotalStatsPoints = 0;

			Event @event = new(name, difficulty);

			for (int i = 0; i < Enum.GetValues<StatsName>().Length; i++)
				actualTotalStatsPoints += @event.Stat.GetStatValue(Enum.GetValues<StatsName>()[i]);

			Assert.Equal(totalStatsPoints, actualTotalStatsPoints);
		}
	}
}
