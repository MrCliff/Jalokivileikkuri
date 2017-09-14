using System;
using System.Collections.Generic;

namespace GemCutter
{
    /// <summary>
    /// Stores a combination of cuts.
    /// </summary>
    class CutCombination : IEquatable<CutCombination>
    {
        /// <summary>
        /// The cuts that belong to this combination.
        /// </summary>
        private ICollection<Cut> Cuts { get; }

        /// <summary>
        /// The sum of the sizes of all the cuts.
        /// </summary>
        public int SizeSum { get; private set; }


        /// <summary>
        /// The total value of all the cuts.
        /// </summary>
        public int Value { get; private set; }


        /// <summary>
        /// Initializes a new CutCombination for use.
        /// </summary>
        public CutCombination()
        {
            SizeSum = 0;
            Value = 0;
            Cuts = new List<Cut>();
        }


        /// <summary>
        /// Initializes a new CutCombination using the cuts in the given old CutCombination.
        /// </summary>
        /// <param name="oldCombination">The old CutCombination.</param>
        public CutCombination(CutCombination oldCombination)
        {
            SizeSum = oldCombination.SizeSum;
            Value = oldCombination.Value;

            Cuts = new List<Cut>();
            foreach (Cut cut in oldCombination.Cuts)
            {
                Cuts.Add(cut);
            }
        }


        /// <summary>
        /// Adds the given cut to this combination.
        /// </summary>
        /// <param name="cut">The cut to be added.</param>
        public void Add(Cut cut)
        {
            Cuts.Add(cut);
            SizeSum += cut.Size;
            Value += cut.Value;
        }


        /// <summary>
        /// Compares this object to the given object and tell's, if they are equal.
        /// </summary>
        /// <param name="other">The object that this object should be compared to.</param>
        /// <returns>Returns true, if the objects are equal.</returns>
        public bool Equals(CutCombination other)
        {
            if (SizeSum != other.SizeSum) return false;
            if (Value != other.Value) return false;
            if (Cuts.Count != other.Cuts.Count) return false;
            return true;
        }


        /// <summary>
        /// Compares this object to the given object and tell's, if they are equal.
        /// </summary>
        /// <param name="obj">The object that this object should be compared to.</param>
        /// <returns>Returns true, if the objects are equal.</returns>
        public override bool Equals(object obj)
        {
            CutCombination cut = obj as CutCombination;
            if (cut == null) return false;
            return Equals(cut);
        }


        public override int GetHashCode()
        {
            var hashCode = -2040343263;
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Cut>>.Default.GetHashCode(Cuts);
            hashCode = hashCode * -1521134295 + SizeSum.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
