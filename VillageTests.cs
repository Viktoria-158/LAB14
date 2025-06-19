using PlantsLibraryVer2;

namespace TestsLab14
{
    [TestClass]
    public class VillageTests
    {
        [TestMethod]
        public void AddGarden_ValidGarden_AddsToDictionary()
        {
            // Arrange
            var village = new Village(1, "Test Village");
            var garden = new Garden(1);

            // Act
            village.AddGarden(garden);

            // Assert
            Assert.AreEqual(1, village.Gardens.Count);
            Assert.IsTrue(village.Gardens.ContainsKey(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddGarden_NullGarden_ThrowsException()
        {
            // Arrange
            var village = new Village(1, "Test Village");

            // Act
            village.AddGarden(null);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var village = new Village(1, "Green Valley");

            // Act
            var result = village.ToString();

            // Assert
            Assert.AreEqual("Деревня: Green Valley (ID: 1), Садов: 0", result);
        }
    }
}
