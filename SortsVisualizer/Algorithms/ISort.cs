using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public interface ISort
    {
        public TimeSpan Time { get; set; }
        public void Sort(List<SortElement> data, Action StateChange);
        public void Stop();
    }
}
