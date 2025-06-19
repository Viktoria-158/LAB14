using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantsLibraryVer2;

namespace TestsLab14
{
    [TestClass]
    public class GardenBedTests
    {
        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var bed = new GardenBed
            {
                BedNumber = 42,
                PlantType = "Роза",
                PlantId = 1
            };

            // Act
            var result = bed.ToString();

            // Assert
            Assert.AreEqual("Грядка №42 (Роза), ID растения: 1", result);
        }
    }
}
