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
    public class RegionRepository : IRegionRepository
    {
        private AnimalDbContext DbContext { get; }

        public RegionRepository()
        {
            DbContext = new AnimalDbContext();
        }

        public Region Add(Region region)
        {
            DbContext.Regions.Add(region);
            DbContext.SaveChanges();
            return region;
        }

        public IEnumerable<Region> GetAll()
        {
            return DbContext.Regions.AsEnumerable();
        }
    }
}
