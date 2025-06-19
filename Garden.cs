using System;
using System.Collections.Generic;

namespace PlantsLibraryVer2
{
    public class Garden
    {
        public int Id { get; set; }
        public Queue<Plant> Plants { get; set; } 

        public Garden(int id)
        {
            Id = id;
            Plants = new Queue<Plant>();
        }

        public void AddPlant(Plant plant)
        {
            if (plant == null)
                throw new ArgumentNullException(nameof(plant));
            Plants.Enqueue(plant); 
        }

        public bool TryPeek(out Plant plant)
        {
            return Plants.TryPeek(out plant); // метод для просмотра первого элемента
        }

        public override string ToString()
        {
            return $"Сад {Id} (Растений: {Plants.Count})";
        }
    }
}