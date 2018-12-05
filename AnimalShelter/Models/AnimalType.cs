using System.Collections.Generic;

namespace AnimalShelter.Models
{
    public class AnimalType
    {
        private static List<AnimalType> _instances = new List<AnimalType> { };
        private string _type;
        private int _animalTypeid;
        private List<Animal> _animals;

        public AnimalType(string type)
        {
            _type = type;
            _instances.Add(this);
            _animalTypeid = _instances.Count;
            _animals = new List<Animal> { };
        }

        public string GetAnimalType()
        {
            return _type;
        }

        public int GetAnimalTypeId()
        {
            return _animalTypeid;
        }

        public void AddItem(Animal animal)
        {
            _animals.Add(animal);
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }

        public static List<AnimalType> GetAll()
        {
            return _instances;
        }

        public static AnimalType Find(int searchId)
        {
            return _instances[searchId - 1];
        }

        public List<Animal> GetAnimals()
        {
            return _animals;
        }

    }
}