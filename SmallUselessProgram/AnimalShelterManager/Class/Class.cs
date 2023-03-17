using System.ComponentModel;

namespace AnimalShelterManagerNamespace
{
    public enum species
    {
        [Description("Dog")]
        dog,
        [Description("Cat")]
        cat,
    }
    public enum MenuOption
    {
        AddDog = 1,
        AddCat,
        MakeAllSounds,
        DisplayAllAnimals,
        RemoveAnimal,
        SearchAnimal,
        Quit
    }
    public interface IAnimal
    {
        string Name { get; set; }
        void MakeSound();
    }

    public class Dog : IAnimal
    {
        public string Name { get; set; }
        public void MakeSound()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class Cat : IAnimal
    {
        public string Name { get; set; }
        public void MakeSound()
        {
            Console.WriteLine("Meow!");
        }
    }

    public class AnimalShelter<T> where T : class, IAnimal, new()
    {
        private List<T> _animals;
        public string Name { get; set; }

        public AnimalShelter(string name)
        {
            Name = name;
            _animals = new List<T>();
        }

        public int TotalAnimals
        {
            get { return _animals.Count; }
        }

        public bool AddAnimal(string name, string species)
        {
            if (IsAnimalExisted(name))
            {
                return false;
            }
            T animal = new T();
            animal.Name = name;
            _animals.Add(animal);
            Console.WriteLine($"{name} ({species}) added to {this.Name}.");
            return true;
        }

        public void MakeAllSounds()
        {
            foreach (var animal in _animals)
            {
                animal.MakeSound();
            }
        }

        public void DisplayAllAnimals()
        {
            foreach (var animal in _animals)
            {
                Console.WriteLine(animal.Name);
            }
        }

        public bool RemoveAnimal(string name)
        {
            T animal = _animals.Find(a => a.Name == name);
            if (animal != null)
            {
                _animals.Remove(animal);
                Console.WriteLine(animal.Name + " removed from the shelter.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SearchAnimal(string name)
        {
            T animal = _animals.Find(a => a.Name == name);
            if (animal != null)
            {
                Console.WriteLine($"{animal.Name} found in {Name}.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<T> Animals
        {
            get { return _animals; }
        }

        public bool IsAnimalExisted(string name)
        {
            return _animals.Any(a => a.Name == name);
        }
    }
}
