using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalokivileikkuri
{
    /// <summary>
    /// The product of a gem cut.
    /// </summary>
    class CutProduct
    {
        ICollection<Cut> cuts;


        /// <summary>
        /// The size of the original raw gem chunk.
        /// </summary>
        private int RawChunkSize { get; set; }


        /// <summary>
        /// The collection of the cuts that should be applied to the RawChunk.
        /// </summary>
        private ICollection<Cut> Cuts
        {
            get { return cuts; }
            set { cuts = value; }
        }


        /// <summary>
        /// The total value of all the cuts.
        /// </summary>
        private int Value
        {
            get { return 0; }
        }


        /// <summary>
        /// The total waste left after all the cuts.
        /// </summary>
        private int Waste
        {
            get { return 0; }
        }


        /// <summary>
        /// The total profit after all the cuts.
        /// </summary>
        private int Profit
        {
            get { return Value - Waste; }
        }


        /// <summary>
        /// Initializes a new CutProduct with the given values.
        /// </summary>
        /// <param name="rawChunkSize">The size of the original raw gem chunk.</param>
        /// <param name="cuts">The collection of the cuts that should be applied to the RawChunk.</param>
        public CutProduct(int rawChunkSize, ICollection<Cut> cuts)
        {
            RawChunkSize = rawChunkSize;
            Cuts = cuts;
        }
    }
}
