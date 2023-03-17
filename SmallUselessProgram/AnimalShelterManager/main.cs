using Helpers;

namespace AnimalShelterManagerNamespace
{
    public class AnimalShelterManager
    {
        AnimalShelter<Dog> dogShelter = new AnimalShelter<Dog>(Helper.GetEnumDescription(species.dog));
        AnimalShelter<Cat> catShelter = new AnimalShelter<Cat>(Helper.GetEnumDescription(species.cat));
        public void Main()
        {
            Console.WriteLine("Welcome to Animal Shelter Manager");
            while (true)
            {
                DisplayMenu();
                Console.Write($"Enter your choice (1-{Enum.GetNames(typeof(MenuOption)).Length}): ");
                if (!int.TryParse(Console.ReadLine(), out int input) || !Enum.IsDefined(typeof(MenuOption), input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
                switch ((MenuOption)input)
                {
                    case MenuOption.AddDog:
                        AddAnimal(dogShelter, species.dog.ToString());
                        break;
                    case MenuOption.AddCat:
                        AddAnimal(catShelter, species.cat.ToString());
                        break;
                    case MenuOption.MakeAllSounds:
                        MakeAllSounds();
                        break;
                    case MenuOption.DisplayAllAnimals:
                        DisplayAllAnimals();
                        break;
                    case MenuOption.RemoveAnimal:
                        RemoveAnimal();
                        break;
                    case MenuOption.SearchAnimal:
                        SearchAnimal();
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Goodbye!");
                        return;
                }
            }
        }
        private void DisplayMenu()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add a dog");
            Console.WriteLine("2. Add a cat");
            Console.WriteLine("3. Make all animal sounds");
            Console.WriteLine("4. Display all animals");
            Console.WriteLine("5. Remove an animal");
            Console.WriteLine("6. Search for an animal");
            Console.WriteLine("7. Quit");
        }
        private void AddAnimal<T>(AnimalShelter<T> animalShelter, string species) where T : class, IAnimal, new()
        {
            Console.WriteLine($"Enter {species} name:");
            string animalName = Console.ReadLine();
            bool added = animalShelter.AddAnimal(animalName, species);
            if (!added)
            {
                Console.WriteLine(animalName + " not found in the shelter.");
            }
        }
        private void MakeAllSounds()
        {
            dogShelter.MakeAllSounds();
            catShelter.MakeAllSounds();
        }
        private void DisplayAllAnimals()
        {
            dogShelter.DisplayAllAnimals();
            catShelter.DisplayAllAnimals();
        }
        private void RemoveAnimal()
        {
            Console.WriteLine("Enter name of animal to remove:");
            string animalName = Console.ReadLine();
            bool removed = dogShelter.RemoveAnimal(animalName);
            if (!removed)
            {
                removed = catShelter.RemoveAnimal(animalName);
            }
            if (!removed)
            {
                Console.WriteLine(animalName + " not found in the shelter.");
            }
        }
        private void SearchAnimal()
        {
            Console.WriteLine("Enter name of animal to search for:");
            string searchName = Console.ReadLine();

            bool found = dogShelter.SearchAnimal(searchName);
            if (!found)
            {
                found = catShelter.SearchAnimal(searchName);
            }
            if (!found)
            {
                Console.WriteLine(searchName + " not found in the shelter.");
            }
        }
    }
}