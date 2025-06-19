using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantsLibraryVer2;

namespace TestsLab14
{
    [TestClass]
    public class ProgramTests
    {
        public Program _program;
        private List<Village> _villages;
        private List<GardenBed> _gardenBeds;

        [TestInitialize]
        public void Setup()
        {
            _program = new Program();
            _villages = new List<Village>();
            _gardenBeds = new List<GardenBed>();

            // Инициализация тестовых данных
            var village = new Village(1, "Test Village");
            var garden = new Garden(1);
            var plant = new Plant { Id = new IdNumber(1), Name = "Роза" };
            garden.AddPlant(plant);
            village.AddGarden(garden);
            _villages.Add(village);

            _gardenBeds.Add(new GardenBed { BedNumber = 1, PlantId = 1, PlantType = "Rose" });
        }

        [TestMethod]
        public void FindPlantById_ExistingId_ReturnsPlant()
        {
            // Arrange
            int existingId = 1;

            // Act
            var plant = _program.FindPlantByIdTestable(_villages, existingId);

            // Assert
            Assert.IsNotNull(plant);
            Assert.AreEqual("Роза", plant.Name);
        }

        [TestMethod]
        public void FindPlantById_NonExistingId_ReturnsNull()
        {
            // Arrange
            int nonExistingId = 999;

            // Act
            var plant = _program.FindPlantByIdTestable(_villages, nonExistingId);

            // Assert
            Assert.IsNull(plant);
        }

        [TestMethod]
        public void ShowPlantBeds_JoinsCorrectly()
        {
            // Arrange
            var expected = "Роза → Грядка №1";

            // Act
            var result = _program.ShowPlantBedsTestable(_villages, _gardenBeds);

            // Assert
            Assert.IsTrue(result.Contains(expected));
        }
}

// В класс Program нужно добавить тестовые методы:
public Plant FindPlantByIdTestable(List<Village> villages, int id)
        {
            var allPlants = villages.SelectMany(v => v.Gardens.Values)
                                  .SelectMany(g => g.Plants)
                                  .ToList();
            return allPlants.FirstOrDefault(p => p.Id.Number == id);
        }

        public string ShowPlantBedsTestable(List<Village> villages, List<GardenBed> beds)
        {
            var joinResult = villages.SelectMany(v => v.Gardens.Values)
                                   .SelectMany(g => g.Plants)
                                   .Join(beds,
                                       p => p.Id.Number,
                                       g => g.PlantId,
                                       (p, g) => $"{p.Name} → Грядка №{g.BedNumber}");
            return string.Join("\n", joinResult);
        }
    }
