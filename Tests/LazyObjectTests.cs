namespace Loxifi.LazyObjects.Tests
{
	[TestClass]
	public class LazyObjectTests
	{
		[TestMethod]
		public void TestLazy()
		{
			Lazy<int> l0 = new(() => 1);

			Assert.AreEqual(1, l0.Value);
		}

		[TestMethod]
		public async Task TestLazyTask()
		{
			Lazy<Task<int>> l0 = new(async () =>
			{
				await Task.Delay(1);
				return 1;
			});
	
			Assert.AreEqual(1, await l0.Value);
		}
	}
}