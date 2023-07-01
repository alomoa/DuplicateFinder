using Moq;

namespace Test
{
    public class Tests
    {
        DuplicateFinder finder;
        [SetUp]
        public void Setup()
        {
            string[] returnValue = new string[] {"hell","hello","help","tep" };
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(x => x.ReadAllLines("/path")).Returns(returnValue);

            finder = new DuplicateFinder(fileReader.Object);
        }

        [Test]
        public void ShouldReturnCorrectNumberOfResults()
        {
            //Arrange //Act
            var results = finder.Execute("/path");

            //Assert

            Assert.That(results.Count, Is.EqualTo(12));

            
        }
        [Test]
        public void ShouldThrowErrorWhenNoPathIsGiven()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => finder.Execute(""));


        }

        [Test]
        public void TopResultShouldBeHelloAndHell()
        {
            //Arrange //Act
            var results = finder.Execute("/path");

            //Assert

            Assert.That(results[0].Target, Is.EqualTo("hello"));
            Assert.That(results[0].Subject, Is.EqualTo("hell"));
        }

        [Test]
        public void ResultsShouldBeOfComparisonScoreType()
        {
            var results = finder.Execute("/path");

            //Assert

            foreach(var result in results)
            {
                Assert.That(result.GetType().Name, Is.EqualTo(typeof(ComparisonScore).Name));
            }
        }
    }
}