using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalokivileikkuri
{
    /// <summary>
    /// A factory that makes Cut-objects that are similar to itself.
    /// </summary>
    class CutFactory : IComparable<CutFactory>
    {
        /// <summary>
        /// The size of the cut.
        /// </summary>
        private int Size { get; set; }


        /// <summary>
        /// The value of the cut.
        /// </summary>
        private int Value { get; set; }


        /// <summary>
        /// Produces a new cut-instance.
        /// </summary>
        private Cut Cut
        {
            get
            {
                return new Cut(Size, Value);
            }
        }


        /// <summary>
        /// Initializes a new factory with the values given as a dictionary.
        /// </summary>
        /// <param name="settings">The size and value of the cuts that this factory will produce.</param>
        public CutFactory(IDictionary<string, int> settings)
        {
            string sizeString = "size";
            string valueString = "value";

            if (settings.Keys.Contains(sizeString)) Size = settings[sizeString];
            if (settings.Keys.Contains(valueString)) Value = settings[valueString];
        }


        /// <summary>
        /// Compares this object to the given object and returns an integer
        /// that represents their order.
        /// </summary>
        /// <param name="other">The object that this object should be compared to.</param>
        /// <returns>A negative number, if the other is greater than this,
        /// a positive number, if the other is less than this and zero, if they are the same.</returns>
        int IComparable<CutFactory>.CompareTo(CutFactory other)
        {
            return Size.CompareTo(other.Size);
        }
    }
}
