using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Pattern_Repository.Models
{
    public class Region
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public IEnumerable<Region> Animals { get; set; } = [];
    }
}
