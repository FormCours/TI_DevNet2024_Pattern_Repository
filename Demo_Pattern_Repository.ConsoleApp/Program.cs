using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;

using ADO = Demo_Pattern_Repository.DatabaseADO;
using DAP = Demo_Pattern_Repository.DatabaseDapper;

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


IFamiliaRepository familiaRepository = new ADO.FamiliaRepository();
IAnimalRepository animalRepository = new DAP.AnimalRepository();

//Console.WriteLine("Liste des familles");
//IEnumerable<Familia> result =  familiaRepository.GetAll();
//foreach (Familia familia in result)
//{
//    Console.WriteLine($"{familia.Id} : {familia.Name}");
//}
//Console.WriteLine();

Console.WriteLine("Liste des animaux");
IEnumerable<Animal> animals = animalRepository.GetAll();
foreach (Animal animal in animals)
{
    Console.WriteLine($"{animal.Id}) Nom: {animal.Name} / Famile: {animal.Familia.Name}");
}
Console.WriteLine();