using System.Collections.Generic;
using System.Linq;

namespace Jalokivileikkuri
{
    /// <summary>
    /// A container that contains all the combination sets of cuts for the
    /// different gem chunk sizes.
    /// </summary>
    class CutCombinationContainer
    {
        /// <summary>
        /// A list of all possible sets of cuts for each individual gem chunk size.
        /// </summary>
        private SortedDictionary<int, IList<CutCombination>> CalculatedCuts { get; }

        /// <summary>
        /// The set of possible cuts for the gem of this cutter.
        /// </summary>
        private SortedSet<Cut> PossibleCuts { get; }

        /// <summary>
        /// All the combinations that are not relevant for the currently biggest
        /// chunk cut size.
        /// </summary>
        private IList<CutCombination> DismissedCombinations { get; set; }


        /// <summary>
        /// Initializes a new CutSetContainer for use.
        /// </summary>
        public CutCombinationContainer(SortedSet<Cut> possibleCuts)
        {
            CalculatedCuts = new SortedDictionary<int, IList<CutCombination>>();
            PossibleCuts = possibleCuts;
        }


        /// <summary>
        /// Returns the correct list of cut sets using the given rawChunkSize.
        /// </summary>
        /// <param name="rawChunkSize">The size for which the list of cut sets
        /// should be returned.</param>
        /// <returns>A list of all the cuts for the given rawChunkSize.</returns>
        public IEnumerable<CutCombination> GetCutCombinations(int rawChunkSize)
        {
            if (CalculatedCuts.Keys.Contains(rawChunkSize)) return CalculatedCuts[rawChunkSize];

            return CalculateCutCombinations(rawChunkSize);
        }


        /// <summary>
        /// Calculates the list of all possible cuts for the given rawChunkSize.
        /// </summary>
        /// <param name="rawChunkSize">The size of chunk for which the list of
        /// cut sets should be calculated.</param>
        /// <returns>The calculated list of cut sets.</returns>
        private IEnumerable<CutCombination> CalculateCutCombinations(int rawChunkSize)
        {
            int biggestCalculatedSize = 0;
            if (CalculatedCuts.Count > 0) biggestCalculatedSize = CalculatedCuts.Last().Key;

            for (int i = biggestCalculatedSize + 1; i <= rawChunkSize; i++)
            {
                List<CutCombination> currentCutCombinations = new List<CutCombination>();
                if (CalculatedCuts.Count > 0) currentCutCombinations.AddRange(CalculatedCuts.Last().Value);

                List<CutCombination> newCutCombinations = new List<CutCombination>();
                foreach (var cutCombination in currentCutCombinations)
                {
                    int totalCutSize = cutCombination.SizeSum;

                    foreach (Cut cut in PossibleCuts)
                    {
                        if (i < totalCutSize + cut.Size) continue;

                        CutCombination newCutCombination = new CutCombination(cutCombination);
                        newCutCombination.Add(cut);

                        if (currentCutCombinations.Contains(newCutCombination)) continue;
                        if (newCutCombinations.Contains(newCutCombination)) continue;

                        //if (currentCutCombinations.ForEach(comb => newCutCombination)) //remove unnecessary cuts

                        newCutCombinations.Add(newCutCombination);
                    }
                }
                currentCutCombinations.AddRange(newCutCombinations);

                // If current chunk size is smaller than or equal to the biggest possible cut size,
                // try to add one of the cuts to the current combination.
                if (i <= PossibleCuts.Max.Size)
                {
                    foreach (Cut cut in PossibleCuts)
                    {
                        if (i != cut.Size) continue;
                        CutCombination newCutCombination = new CutCombination();
                        newCutCombination.Add(cut);

                        currentCutCombinations.Add(newCutCombination);
                    }
                }

                CalculatedCuts.Add(i, currentCutCombinations);
            }

            return CalculatedCuts[rawChunkSize];
        }
    }
}
