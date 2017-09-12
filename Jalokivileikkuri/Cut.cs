using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalokivileikkuri
{
    /// <summary>
    /// A possible cut for a gem.
    /// </summary>
    class Cut
    {
        private int size;
        private int value;


        /// <summary>
        /// The size of the cut.
        /// </summary>
        public int Size
        {
            get { return size; }
            private set { size = value; }
        }


        /// <summary>
        /// The value of the cut.
        /// </summary>
        public int Value
        {
            get { return value; }
            private set { this.value = value; }
        }


        /// <summary>
        /// Initializes a new cut object.
        /// </summary>
        /// <param name="size">The size of the cut.</param>
        /// <param name="value">The value of the cut.</param>
        public Cut(int size, int value)
        {
            Size = size;
            Value = value;
        }


        /// <summary>
        /// Substracts the size of this cut from the given chunkSize and returns the
        /// remaining size.
        /// </summary>
        /// <param name="chunkSize">The size of the cuttable chunk.</param>
        /// <returns>The remaining chunkSize.</returns>
        public int calculateCut(int chunkSize)
        {
            return chunkSize - Size;
        }
    }
}
