using System.Text;

namespace Jalokivileikkuri
{
    /// <summary>
    /// The product of a gem cut.
    /// </summary>
    class CutProduct
    {
        /// <summary>
        /// The size of the original raw gem chunk.
        /// </summary>
        private int RawChunkSize { get; }
        
        /// <summary>
        /// The collection of the cuts that should be applied to the RawChunk.
        /// </summary>
        private CutCombination Cuts { get; }
        
        /// <summary>
        /// The total value of all the cuts.
        /// </summary>
        public int Value
        {
            get
            {
                return Cuts.Value;
            }
        }
        
        /// <summary>
        /// The total waste left after all the cuts.
        /// </summary>
        public int Waste
        {
            get
            {
                return RawChunkSize - Cuts.SizeSum;
            }
        }
        
        /// <summary>
        /// The total profit after all the cuts.
        /// </summary>
        public int Profit
        {
            get { return Value - Waste; }
        }


        /// <summary>
        /// Initializes a new CutProduct with the given values.
        /// </summary>
        /// <param name="rawChunkSize">The size of the original raw gem chunk.</param>
        /// <param name="cuts">The collection of the cuts that should be applied to the RawChunk.</param>
        public CutProduct(int rawChunkSize, CutCombination cuts)
        {
            RawChunkSize = rawChunkSize;
            Cuts = cuts;
        }


        /// <summary>
        /// Gives the string representation of this CutProduct.
        /// </summary>
        /// <returns>The string representation of this object.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Value: " + Value);
            result.Append(", Waste: " + Waste);
            result.Append(", Profit: " + Profit);
            //result.Append(", Cuts: " + Cuts.ToString());

            return result.ToString();
        }
    }
}
