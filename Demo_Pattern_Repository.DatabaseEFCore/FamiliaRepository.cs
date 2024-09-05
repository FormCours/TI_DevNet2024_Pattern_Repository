using Demo_Pattern_Repository.DatabaseEFCore.Config;
using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Pattern_Repository.DatabaseEFCore
{
    public class FamiliaRepository : IFamiliaRepository
    {
        private AnimalDbContext DbContext { get; }

        public FamiliaRepository()
        {
            DbContext = new AnimalDbContext();
        }

        public Familia Add(Familia familia)
        {
            DbContext.Familias.Add(familia);
            DbContext.SaveChanges();
            return familia;
        }

        public IEnumerable<Familia> GetAll()
        {
            foreach (var familia in DbContext.Familias)
            {
                yield return familia;
            }
        }

        public Familia? GetById(int id)
        {
            return DbContext.Familias.SingleOrDefault(f => f.Id == id);
        }
    }
}
