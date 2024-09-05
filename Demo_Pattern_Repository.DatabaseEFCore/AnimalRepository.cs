using Demo_Pattern_Repository.DatabaseEFCore.Config;
using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Pattern_Repository.DatabaseEFCore
{
    public class AnimalRepository : IAnimalRepository
    {
        private AnimalDbContext DbContext { get; }

        public AnimalRepository()
        {
            DbContext = new AnimalDbContext();
        }

        public Animal? GetById(int id)
        {
            var result = DbContext.Animals
                                  .Include(a => a.Familia)
                                  .Include(a => a.Regions)
                                  .Where(a => a.Id == id);

            return result.SingleOrDefault();
        }

        public IEnumerable<Animal> GetAll()
        {
            foreach (Animal animal in DbContext.Animals)
            {
                yield return animal;
            }
        }

        public IEnumerable<Animal> GetByFamilia(string familia)
        {
            var result = DbContext.Animals
                                .Include (a => a.Familia)
                                .Where(a => a.Familia.Name == familia);

            foreach (Animal animal in result)
            {
                yield return animal;
            }
        }

        public IEnumerable<Animal> GetFromRegion(string region)
        {
            var result = DbContext.Animals
            .Include(a => a.Familia)
            .Where(a => a.Regions.Any(r => r.Name == region));

            foreach (Animal animal in result)
            {
                yield return animal;
            }
        }

        public Animal Add(Animal animal)
        {
            DbContext.Animals.Add(animal);
            DbContext.SaveChanges();
            return animal;
        }

        public bool Remove(int id)
        {
            Animal? a = DbContext.Animals
                            .SingleOrDefault(a => a.Id == id);

            if(a is not null) {
                DbContext.Animals.Remove(a);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
