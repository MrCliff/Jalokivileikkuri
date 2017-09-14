using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalokivileikkuri
{
    /// <summary>
    /// A possible cut for a gem.
    /// </summary>
    class Cut : IComparable<Cut>, IEquatable<Cut>
    {
        /// <summary>
        /// The size of the cut.
        /// </summary>
        public int Size { get; }
        
        /// <summary>
        /// The value of the cut.
        /// </summary>
        public int Value { get; }


        /// <summary>
        /// Initializes a new cut with the values given as a dictionary.
        /// </summary>
        /// <param name="settings">The size and value of this cut as a dictionary.
        /// The dictionary should have at least keys "size" and "value".</param>
        public Cut(IDictionary<string, JsonValue> settings)
        {
            string sizeString = "size";
            string valueString = "value";

            if (settings.Keys.Contains(sizeString)) Size = settings[sizeString];
            if (settings.Keys.Contains(valueString)) Value = settings[valueString];
        }

        
        /// <summary>
        /// Gives the string representation of this Cut.
        /// </summary>
        /// <returns>The string representation of this object.</returns>
        public override string ToString()
        {
            return "Size: " + Size + ", Value: " + Value;
        }


        /// <summary>
        /// Compares this object to the given object and returns an integer
        /// that represents their order.
        /// </summary>
        /// <param name="other">The object that this object should be compared to.</param>
        /// <returns>A negative number, if the other is greater than this,
        /// a positive number, if the other is less than this and zero, if they are the same.</returns>
        public int CompareTo(Cut other)
        {
            int result = Size.CompareTo(other.Size);
            if (result != 0) return result;

            result = Value.CompareTo(other.Value);
            return result;
        }


        /// <summary>
        /// Compares this object to the given object and tell's, if they are equal.
        /// </summary>
        /// <param name="other">The object that this object should be compared to.</param>
        /// <returns>Returns true, if the objects are equal.</returns>
        public bool Equals(Cut other)
        {
            return Size == other.Size && Value == other.Value;
        }


        /// <summary>
        /// Compares this object to the given object and tell's, if they are equal.
        /// </summary>
        /// <param name="obj">The object that this object should be compared to.</param>
        /// <returns>Returns true, if the objects are equal.</returns>
        public override bool Equals(object obj)
        {
            Cut cut = obj as Cut;
            if (cut == null) return false;
            return Equals(cut);
        }


        public override int GetHashCode()
        {
            var hashCode = 1882781510;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
