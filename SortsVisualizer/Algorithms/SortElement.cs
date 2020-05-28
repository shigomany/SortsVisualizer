using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public class SortElement : IComparable<SortElement>
    {
        public int Value { get; set; }
        public List<byte> Data { get; set; }

        public MagickImage GetImage()
        {
            return new MagickImage(Data.ToArray());
        }

        public string GetBase64()
        {
            return Convert.ToBase64String(Data.ToArray());
        }

        public int CompareTo([AllowNull] SortElement other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
