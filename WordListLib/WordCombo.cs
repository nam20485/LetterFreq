using System;
using System.Collections.Generic;
using System.Text;

using static WordListLib.WordList;

namespace WordListLib
{
    public class WordCombo : IEquatable<WordCombo>
    {
        public List<string> Words;

        public WordCombo(IEnumerable<string> words)
        {
            Words = new List<string>(words);
        }

        public bool Equals(WordCombo other)
        {
            // comparing to null?
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            // same exact object?
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            // equal to another combo if the list of words are the same, ignoring order
            return this.Words.SequenceEqual(other.Words, true);
        }

        public override bool Equals(object obj)
        {
            //// different type?
            //if (obj.GetType() != this.GetType())
            //{
            //    return false;
            //}

            // call our value-semantics interface override
            return this.Equals(obj as WordCombo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int PRIME_NUM_SEED = 13;
                const int PRIME_NUM_HASH = 397;

                var hashCode = PRIME_NUM_SEED;
                hashCode = (hashCode * PRIME_NUM_HASH) ^ Words.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(WordCombo w1, WordCombo w2)
        {
            if (w1 != null)
            {
                return w1.Equals(w2);
            }

            return false;
        }

        public static bool operator !=(WordCombo w1, WordCombo w2)
        {
            return !(w1 == w2);
        }

        public class List : List<WordCombo>
        {                        
            public WordList OriginalList
            {
                get;
            }

            private List(WordList originalList)
            {
                OriginalList = originalList;
            }

            public WordComboScore.List Score(WordScorerBase wordScorer)
            {
                return wordScorer.ScoreWordList(this);
            }

            public static List Make(WordList originalList)
            {               
                var list = new List(originalList);

                for (int i = 0; i < originalList.Count; i++)
                {
                    for (int j = i + 1; j < originalList.Count; j++)
                    {
                        list.Add(new WordCombo(new[]
                                                    {
                                                        originalList[i],
                                                        originalList[j]
                                                    }));
                    }
                }

                return list;
            }
        }
    }
}
