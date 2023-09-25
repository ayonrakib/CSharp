namespace Selenium.Learning
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("This is the setup.");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("This is the test1.");
        }
        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("This is the test2.");
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("This is the teardown.");
        }
    }
}