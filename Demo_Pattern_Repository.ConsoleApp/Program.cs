using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;

using ADO = Demo_Pattern_Repository.DatabaseADO;
using DAP = Demo_Pattern_Repository.DatabaseDapper;
using EFC = Demo_Pattern_Repository.DatabaseEFCore;

/*
IEnumerable<int> GetNumber(int limit)
{
    int value = 0;

    while(value < limit)
    {
        yield return value++;
    }
}

IEnumerable<int> res1 =  GetNumber(10);
IEnumerable<int> res2 =  GetNumber(100);
IEnumerable<int> res3 =  GetNumber(1_000);
IEnumerable<int> res4 =  GetNumber(10_000);

Console.WriteLine("Test");

foreach(int val in res2)
{
    Console.WriteLine($" - {val}");
}
*/

Console.WriteLine("Démo du pattern Repository");
Console.WriteLine("**************************");
Console.WriteLine();


//IFamiliaRepository familiaRepository = new EFC.FamiliaRepository();
//IAnimalRepository animalRepository   = new EFC.AnimalRepository();
//IRegionRepository regionRepository   = new EFC.RegionRepository();


//Console.WriteLine("Liste des familles");
//IEnumerable<Familia> result =  familiaRepository.GetAll();
//foreach (Familia familia in result)
//{
//    Console.WriteLine($"{familia.Id} : {familia.Name}");
//}
//Console.WriteLine();


//Console.WriteLine("Liste des animaux");
//IEnumerable<Animal> animals = animalRepository.GetAll();
//foreach (Animal animal in animals)
//{
//    Console.WriteLine($"{animal.Id}) Nom: {animal.Name}");
//    Console.WriteLine($"Famile: {animal.Familia.Name}");
//    Console.WriteLine($"Domestique: {animal.IsDomesticated}");
//}
//Console.WriteLine();


//Console.WriteLine("Liste des animaux par famille");
//IEnumerable<Animal> animals = animalRepository.GetByFamilia("Felidae");
//foreach (Animal animal in animals)
//{
//    Console.WriteLine($"{animal.Id}) Nom: {animal.Name}");
//    Console.WriteLine($"Famile: {animal.Familia.Name}");
//    Console.WriteLine($"Domestique: {animal.IsDomesticated}");
//}
//Console.WriteLine();


//Console.WriteLine("Liste des animaux par region");
//IEnumerable<Animal> animals = animalRepository.GetFromRegion("Europe");
//foreach (Animal animal in animals)
//{
//    Console.WriteLine($"{animal.Id}) Nom: {animal.Name}");
//    Console.WriteLine($"Famile: {animal.Familia.Name}");
//    Console.WriteLine($"Domestique: {animal.IsDomesticated}");
//}
//Console.WriteLine();


//Console.WriteLine("Ajouter une région");
//Region r = regionRepository.Add(new Region() { Name = "Océanie" });
//Console.WriteLine($"> Region ajouter : {r.Id} {r.Name}");

//Console.WriteLine("Liste des animaux d'europe : ");
//IEnumerable<Animal> animalsRegion1 = animalRepository.GetFromRegion("Europe");
//foreach (Animal animal in animalsRegion1)
//{
//    Console.WriteLine($"{animal.Id}) Nom: {animal.Name} / Famile: {animal.Familia.Name}");
//}
//Console.WriteLine();


//Console.WriteLine("Liste des animaux d'asie : ");
//IEnumerable<Animal> animalsRegion2 = animalRepository.GetFromRegion("Asie");
//foreach (Animal animal in animalsRegion2)
//{
//    Console.WriteLine($"{animal.Id}) Nom: {animal.Name} / Famile: {animal.Familia.Name}");
//}
//Console.WriteLine();


//Console.WriteLine("Info d'un animal");
//Animal? animal = animalRepository.GetById(4);

//if (animal is not null)
//{
//    Console.WriteLine($"Nom : {animal.Name}");
//    Console.WriteLine($"Famille : {animal.Familia.Name}");
//    Console.WriteLine($"Region : {string.Join(", ", animal.Regions.Select(r => r.Name))}");
//}
//else
//{
//    Console.WriteLine("J'ai po trouvé !");
//}

