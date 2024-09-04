using Demo_Pattern_Repository.Models;

namespace Demo_Pattern_Repository.Repositories
{
    public interface IAnimalRepository
    {
        Animal? GetById(int id);
        IEnumerable<Animal> GetAll();
        IEnumerable<Animal> GetByFamilia(string familia);
        IEnumerable<Animal> GetFromRegion(string region);

        Animal Add(Animal animal);
        bool Remove(int id);
    }
}
