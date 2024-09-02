using Demo_Pattern_Repository.Models;

namespace Demo_Pattern_Repository.Repositories
{
    public interface IFamiliaRepository
    {
        Familia? GetById(int id);
        IEnumerable<Familia> GetAll();

        Familia Add(Familia familia);
    }
}
