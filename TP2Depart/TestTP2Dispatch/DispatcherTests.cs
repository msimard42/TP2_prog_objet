using TP2Dispatch;

namespace TestTP2Dispatch
{
	public class DispatcherTests
	{
		[Fact]
		public void Constructor_Void_Ok()
		{
			Dispatcher dispatcher = new();

			Assert.Equal(0, dispatcher.NbFailure);
			Assert.Equal(0, dispatcher.NbSuccess);
			Assert.Equal(1, dispatcher.GlobalDifficulty);
			Assert.Equal(5, dispatcher.HeroesAvailable.Length);
		}
	}
}
