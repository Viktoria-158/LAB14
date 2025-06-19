using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantsLibraryVer2;

namespace TestsLab14
{
    [TestClass]
    public class GardenTests
    {
        [TestMethod]
        public void AddPlant_ValidPlant_EnqueuesPlant()
        {
            // Arrange
            var garden = new Garden(1);
            var plant = new Plant { Name = "Роза" };

            // Act
            garden.AddPlant(plant);

            // Assert
            Assert.AreEqual(1, garden.Plants.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddPlant_NullPlant_ThrowsException()
        {
            // Arrange
            var garden = new Garden(1);

            // Act
            garden.AddPlant(null);
        }

        [TestMethod]
        public void TryPeek_WithPlants_ReturnsFirstPlant()
        {
            // Arrange
            var garden = new Garden(1);
            var plant1 = new Plant { Name = "Роза" };
            var plant2 = new Plant { Name = "Тюльпан" };
            garden.AddPlant(plant1);
            garden.AddPlant(plant2);

            // Act
            bool result = garden.TryPeek(out Plant firstPlant);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Роза", firstPlant.Name);
        }

        [TestMethod]
        public void TryPeek_EmptyQueue_ReturnsFalse()
        {
            // Arrange
            var garden = new Garden(1);

            // Act
            bool result = garden.TryPeek(out Plant firstPlant);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(firstPlant);
        }
    }
}
