using System;
using System.Collections.Generic;

namespace PlantsLibraryVer2
{
    public class Village
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, Garden> Gardens { get; set; }

        public Village(int id, string name)
        {
            Id = id;
            Name = name;
            Gardens = new Dictionary<int, Garden>();
        }

        public void AddGarden(Garden garden)
        {
            if (garden == null)
                throw new ArgumentNullException(nameof(garden));
            Gardens.Add(garden.Id, garden);
        }

        public override string ToString()
        {
            return $"Деревня: {Name} (ID: {Id}), Садов: {Gardens.Count}";
        }
    }
}