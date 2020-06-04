using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public class SortData
    {
        public string Name { get; set; }
        public List<SortElement> SectorsList { get; set; }
        public ISort Sorter { get; set; }
    }
}
