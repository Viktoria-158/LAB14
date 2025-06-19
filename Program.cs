using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlantsLibraryVer2
{
    class Program
    {
        private static Random rnd = new Random();
        private static List<Village> villages = new List<Village>();
        private static List<GardenBed> gardenBeds = new List<GardenBed>();
        private static MyCollection<Plant> plantCollection = new MyCollection<Plant>();

        static void Main(string[] args)
        {
            InitializeCollections();
            InitializePlantCollection();
            ShowMainMenu();
        }

        static void InitializeCollections()
        {
            for (int i = 1; i <= 3; i++)
            {
                Village village = new Village(i, $"Деревня {i}");

                for (int j = 1; j <= rnd.Next(2, 5); j++)
                {
                    Garden garden = new Garden(j);

                    for (int k = 0; k < rnd.Next(3, 7); k++)
                    {
                        Plant plant = GenerateRandomPlant();
                        garden.AddPlant(plant);

                        gardenBeds.Add(new GardenBed
                        {
                            BedNumber = rnd.Next(1, 100),
                            PlantType = plant.GetType().Name,
                            PlantId = plant.Id.Number
                        });
                    }
                    village.AddGarden(garden);
                }
                villages.Add(village);
            }
        }

        static void InitializePlantCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                Plant plant = GenerateRandomPlant();
                plantCollection.Add(plant);
            }
        }
        static Plant GenerateRandomPlant()
        {
            int type = rnd.Next(4);
            Plant plant = type switch
            {
                0 => new Plant(),
                1 => new Flower(),
                2 => new Rose(),
                3 => new Tree(),
                _ => new Plant()
            };
            plant.RandomInit();
            return plant;
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nГЛАВНОЕ МЕНЮ:");
                Console.WriteLine("1. Работа с деревнями");
                Console.WriteLine("2. Работа с растениями");
                Console.WriteLine("3. Тест производительности");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите пункт: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ShowVillageMenu();
                        break;
                    case 2:
                        ShowPlantMenu();
                        break;
                    case 3:
                        TestPerformance();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        static void ShowVillageMenu()
        {
            while (true)
            {
                Console.WriteLine("\nРАБОТА С ДЕРЕВНЯМИ");
                Console.WriteLine("1. Создать случайную деревню");
                Console.WriteLine("2. Показать первые растения в садах");
                Console.WriteLine("3. Найти растение по ID");
                Console.WriteLine("4. Статистика по деревьям");
                Console.WriteLine("5. Группировка растений по цвету");
                Console.WriteLine("6. Соединение растений с грядками");
                Console.WriteLine("7. Показать все деревни");
                Console.WriteLine("8. Вернуться в главное меню");
                Console.Write("Выберите пункт: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateRandomVillage();
                        break;
                    case 2:
                        ShowFirstPlants();
                        break;
                    case 3:
                        FindPlantById();
                        break;
                    case 4:
                        ShowTreeStatistics();
                        break;
                    case 5:
                        GroupPlantsByColor();
                        break;
                    case 6:
                        ShowPlantBeds();
                        break;
                    case 7:
                        ShowAllVillages();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        static void ShowPlantMenu()
        {
            while (true)
            {
                Console.WriteLine("\nРАБОТА С РАСТЕНИЯМИ");
                Console.WriteLine("1. Показать все растения");
                Console.WriteLine("2. Показать высокие деревья (>50м)");
                Console.WriteLine("3. Запросы к MyCollection");
                Console.WriteLine("4. Вернуться в главное меню");
                Console.Write("Выберите пункт: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ShowAllPlants();
                        break;
                    case 2:
                        ShowTallTrees();
                        break;
                    case 3:
                        ShowMyCollectionMenu();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        static void ShowMyCollectionMenu()
        {
            while (true)
            {
                Console.WriteLine("\nЗАПРОСЫ К MyCollection");
                Console.WriteLine("1. Выборка данных (Where)");
                Console.WriteLine("2. Получение счетчика (Count)");
                Console.WriteLine("3. Агрегирование данных (Max, Min, Average)");
                Console.WriteLine("4. Группировка данных (GroupBy)");
                Console.WriteLine("5. Сравнение LINQ и методов расширения по времени");
                Console.WriteLine("6. Вернуться в меню растений");
                Console.Write("Выберите пункт: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RunWhereQuery();
                        break;
                    case 2:
                        RunCountQuery();
                        break;
                    case 3:
                        RunAggregationQuery();
                        break;
                    case 4:
                        RunGroupByQuery();
                        break;
                    case 5:
                        CompareQueryStyles();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        static void CreateRandomVillage()
        {
            int id = villages.Count + 1;//номер
            Village village = new Village(id, $"Деревня {id}");

            for (int i = 1; i <= rnd.Next(2, 5); i++)
            {
                Garden garden = new Garden(i);
                for (int j = 0; j < rnd.Next(3, 7); j++)
                {
                    garden.AddPlant(GenerateRandomPlant());
                }
                village.AddGarden(garden);
            }

            villages.Add(village);
            Console.WriteLine($"Создана {village}");
        }

        static void ShowFirstPlants()
        {
            Console.WriteLine("\nПервые растения в очередях садов:");
            foreach (var village in villages)
            {
                Console.WriteLine($"\n{village.Name}:");
                foreach (var garden in village.Gardens.Values)
                {
                    if (garden.TryPeek(out Plant firstPlant))
                    {
                        Console.WriteLine($"- Сад {garden.Id}: {firstPlant.Name}");
                    }
                }
            }
        }

        //linq-запрос
        static void FindPlantById()
        {
            //все растения и их ID
            var allPlants = villages.SelectMany(v => v.Gardens.Values)
                                  .SelectMany(g => g.Plants)
                                  .ToList();

            if (!allPlants.Any())
            {
                Console.WriteLine("\nНет растений для поиска");
                return;
            }

            //вывод доступных ID с группировкой по деревням
            Console.WriteLine("\nДоступные растения (ID 0 Название):");
            foreach (var village in villages)
            {
                Console.WriteLine($"\n{village.Name}:");
                foreach (var garden in village.Gardens.Values)
                {
                    foreach (var plant in garden.Plants)
                    {
                        Console.WriteLine($"- ID: {plant.Id.Number} - {plant.Name} ({plant.GetType().Name})");
                    }
                }
            }

            int id;
            while (true)
            {
                Console.Write("\nВведите ID растения (или 0 для отмены): ");
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Ошибка: введите целое число");
                    continue;
                }

                if (id == 0) return; //выход по 0

                if (allPlants.Any(p => p.Id.Number == id)) break;
                Console.WriteLine("Растения с таким ID не существует");
            }

            // Поиск и вывод результата
            var foundPlant = allPlants.First(p => p.Id.Number == id);

            Console.WriteLine("\nРезультат поиска:");
            Console.WriteLine($"- Тип: {foundPlant.GetType().Name}");
            Console.WriteLine($"- Название: {foundPlant.Name}");
            Console.WriteLine($"- Цвет: {foundPlant.Color}");

            //вывод особых свойств для разных типов растений
            switch (foundPlant)
            {
                case Rose rose:
                    Console.WriteLine($"- Шипы: {(rose.HasThorns ? "Есть" : "Нет")}");
                    Console.WriteLine($"- Запах: {rose.Smell}");
                    break;
                case Flower flower:
                    Console.WriteLine($"- Запах: {flower.Smell}");
                    break;
                case Tree tree:
                    Console.WriteLine($"- Высота: {tree.Height} м");
                    break;
            }
        }

        static void ShowTreeStatistics()
        {
            var trees = villages.SelectMany(v => v.Gardens.Values)
                              .SelectMany(g => g.Plants)
                              .OfType<Tree>();

            if (!trees.Any())
            {
                Console.WriteLine("\nДеревья не найдены!");
                return;
            }

            Console.WriteLine("\nСтатистика по деревьям:");
            Console.WriteLine($"- Количество: {trees.Count()}");
            Console.WriteLine($"- Максимальная высота: {trees.Max(t => t.Height)} м");
            Console.WriteLine($"- Средняя высота: {trees.Average(t => t.Height):F1} м");
        }

        static void GroupPlantsByColor()
        {
            var groups = villages.SelectMany(v => v.Gardens.Values)
                               .SelectMany(g => g.Plants)
                               .GroupBy(p => p.Color);

            Console.WriteLine("\nГруппировка растений по цвету:");
            foreach (var group in groups)
            {
                Console.WriteLine($"\nЦвет: {group.Key} ({group.Count()} шт.)");
                foreach (var plant in group.Take(3))
                {
                    Console.WriteLine($"- {plant.Name} ({plant.GetType().Name})");
                }
            }
        }

        static void ShowPlantBeds()
        {
            var joinResult = villages.SelectMany(v => v.Gardens.Values)
                                   .SelectMany(g => g.Plants)
                                   .Join(gardenBeds,
                                       p => p.Id.Number,
                                       g => g.PlantId,
                                       (p, g) => new {
                                           PlantName = p.Name,
                                           BedNumber = g.BedNumber
                                       });

            Console.WriteLine("\nРастения и их грядки:");
            foreach (var item in joinResult.Take(10))
            {
                Console.WriteLine($"- {item.PlantName} → Грядка №{item.BedNumber}");
            }
        }

        static void TestPerformance()
        {
            int iterations = 10000;
            var stopwatch = new Stopwatch();

            Console.WriteLine("\nТЕСТ ПРОИЗВОДИТЕЛЬНОСТИ");
            Console.WriteLine("Запрос: найти все высокие деревья (>50 метров)");
            Console.WriteLine($"Количество итераций: {iterations}\n");

            //запрос
            var allPlants = villages.SelectMany(v => v.Gardens.Values)
                                  .SelectMany(g => g.Plants)
                                  .OfType<Tree>()
                                  .ToList();

            // 1. LINQ-запрос
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var linqQuery = from tree in allPlants
                                where tree.Height > 50
                                select tree;
                var result = linqQuery.ToList();
            }
            stopwatch.Stop();
            long linqTime = stopwatch.ElapsedTicks;

            // 2. Методы расширения
            stopwatch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                var extQuery = allPlants
                             .Where(tree => tree.Height > 50);
                var result = extQuery.ToList();
            }
            stopwatch.Stop();
            long extTime = stopwatch.ElapsedTicks;

            Console.WriteLine("\nРЕЗУЛЬТАТЫ");
            Console.WriteLine($"LINQ-запрос:       {linqTime} тиков");
            Console.WriteLine($"Методы расширения: {extTime} тиков");
            Console.WriteLine($"Разница:           {Math.Abs(linqTime - extTime)} тиков");

        }

        static void ShowAllVillages()
        {
            Console.WriteLine("\nВсе деревни:");
            foreach (var village in villages)
            {
                Console.WriteLine($"\n{village}");
                foreach (var garden in village.Gardens.Values)
                {
                    Console.WriteLine($"- {garden}");
                }
            }
        }
        static void ShowAllPlants()
        {
            var allPlants = villages.SelectMany(v => v.Gardens.Values)
                                  .SelectMany(g => g.Plants);

            Console.WriteLine("\nВсе растения:");
            foreach (var plant in allPlants)
            {
                Console.WriteLine($"- {plant}");
            }
        }

        static void ShowTallTrees()
        {
            var tallTrees = villages.SelectMany(v => v.Gardens.Values)
                                  .SelectMany(g => g.Plants)
                                  .OfType<Tree>()
                                  .Where(t => t.Height > 50);

            Console.WriteLine("\nВысокие деревья (>50м):");
            foreach (var tree in tallTrees)
            {
                Console.WriteLine($"- {tree.Name} ({tree.Height} м)");
            }
        }

        //2 часть
        static void RunWhereQuery()
        {
            Console.WriteLine("\n1. Выборка растений с красным цветом (Where):");

            // LINQ-запрос
            var linqQuery = from plant in plantCollection
                            where plant.Color == "Красный"
                            select plant;
            Console.WriteLine("\nLINQ-запрос:");
            foreach (var plant in linqQuery)
            {
                Console.WriteLine($"- {plant.Name} ({plant.Color})");
            }

            // Метод расширения
            var extQuery = plantCollection.Where(p => p.Color == "Красный");
            Console.WriteLine("\nМетод расширения:");
            foreach (var plant in extQuery)
            {
                Console.WriteLine($"- {plant.Name} ({plant.Color})");
            }
        }

        static void RunCountQuery()
        {
            Console.WriteLine("\n2. Количество деревьев в коллекции (Count):");

            // LINQ-запрос
            int linqCount = (from plant in plantCollection
                             where plant is Tree
                             select plant).Count();
            Console.WriteLine($"LINQ-запрос: {linqCount} деревьев");

            //методы расширения
            int extCount = plantCollection.Count(p => p is Tree);
            Console.WriteLine($"Методы расширения: {extCount} деревьев");
        }

        static void RunAggregationQuery()
        {
            Console.WriteLine("\n3. Агрегирование данных (Max, Min, Average):");
            var trees = plantCollection.OfType<Tree>().ToList();

            if (trees.Count == 0)
            {
                Console.WriteLine("Деревья не найдены");
                return;
            }

            // LINQ-запрос
            var linqMax = (from tree in trees select tree.Height).Max();
            var linqMin = (from tree in trees select tree.Height).Min();
            var linqAvg = (from tree in trees select tree.Height).Average();
            Console.WriteLine("\nLINQ-запрос:");
            Console.WriteLine($"- Макс. высота: {linqMax} м");
            Console.WriteLine($"- Мин. высота: {linqMin} м");
            Console.WriteLine($"- Средняя высота: {linqAvg:F1} м");

            // Методы расширения
            var extMax = trees.Max(t => t.Height);
            var extMin = trees.Min(t => t.Height);
            var extAvg = trees.Average(t => t.Height);
            Console.WriteLine("\nМетоды расширения:");
            Console.WriteLine($"- Макс. высота: {extMax} м");
            Console.WriteLine($"- Мин. высота: {extMin} м");
            Console.WriteLine($"- Средняя высота: {extAvg:F1} м");
        }

        static void RunGroupByQuery()
        {
            Console.WriteLine("\n4. Группировка растений по цвету (GroupBy):");

            // LINQ-запрос
            var linqGroups = from plant in plantCollection
                             group plant by plant.Color into colorGroup
                             select colorGroup;
            Console.WriteLine("\nLINQ-запрос:");
            foreach (var group in linqGroups)
            {
                Console.WriteLine($"Цвет: {group.Key} ({group.Count()} шт.)");
                foreach (var plant in group.Take(3))
                {
                    Console.WriteLine($"- {plant.Name}");
                }
            }

            //методы расширения
            var extGroups = plantCollection.GroupBy(p => p.Color);
            Console.WriteLine("\nМетоды расширения:");
            foreach (var group in extGroups)
            {
                Console.WriteLine($"Цвет: {group.Key} ({group.Count()} шт.)");
                foreach (var plant in group.Take(3))
                {
                    Console.WriteLine($"- {plant.Name}");
                }
            }
        }

        static void CompareQueryStyles()
        {
            int iterations = 10000;
            var stopwatch = new Stopwatch();
            Console.WriteLine("Запрос: выборка деревьев высотой >50 метров");

            // 1. LINQ-запрос
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var linqQuery = from plant in plantCollection
                                where plant is Tree tree && tree.Height > 50
                                select plant;
                var result = linqQuery.ToList();
            }
            stopwatch.Stop();
            long linqTime = stopwatch.ElapsedTicks;

            //2. Методы расширения
            stopwatch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                var extQuery = plantCollection.Where(p => p is Tree tree && tree.Height > 50);
                var result = extQuery.ToList();
            }
            stopwatch.Stop();
            long extTime = stopwatch.ElapsedTicks;

            Console.WriteLine($"\nLINQ-запрос: {linqTime} тиков");
            Console.WriteLine($"Методы расширения: {extTime} тиков");
            Console.WriteLine($"Разница: {Math.Abs(linqTime - extTime)} тиков");
        }
    }
}