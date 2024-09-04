using Demo_Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Pattern_Repository.Repositories
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();
        Region Add(Region region);
    }
}
