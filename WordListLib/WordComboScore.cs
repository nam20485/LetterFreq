using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Numerics;
using System.Text;

namespace WordListLib
{
    public class WordComboScore : IEquatable<WordComboScore>, IComparable<WordComboScore>
    {
        public WordCombo WordCombo { get; }

        public int Score { get; }

        public WordComboScore(WordCombo words, int score)
        {
            WordCombo = words;
            Score = score;
        }

        public bool Equals(WordComboScore other)
        {
            // comparing to null?
            if (other is null)
            {
                return false;
            }

            // same exact object?
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return Score == other.Score && 
                   WordCombo == other.WordCombo;
        }

        public override bool Equals(object obj)
        {            
            //// different type?
            //if (obj.GetType() != this.GetType())
            //{
            //    return false;
            //}

            // call our value-semantics interface override
            return this.Equals(obj as WordComboScore);
        }

        public static bool operator ==(WordComboScore w1, WordComboScore w2)
        {          
            if (w1 is null)
            {
                return false;
            }
            else
            {
                return w1.Equals(w2);
            }
        }

        public static bool operator !=(WordComboScore w1, WordComboScore w2)
        {
            return !(w1 == w2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int PRIME_NUM_SEED = 13;
                const int PRIME_NUM_HASH = 397;

                var hashCode = PRIME_NUM_SEED;
                hashCode = (hashCode * PRIME_NUM_HASH) ^ Score.GetHashCode();
                hashCode = (hashCode * PRIME_NUM_HASH) ^ WordCombo.GetHashCode();               
                return hashCode;
            }
        }

        public int CompareTo(WordComboScore other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other is null)
            {
                return 1;
            }
            else
            {
                return this.Score.CompareTo(other.Score);
            }
        }

        public static bool operator >(WordComboScore operand1, WordComboScore operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        // Define the is less than operator.
        public static bool operator <(WordComboScore operand1, WordComboScore operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(WordComboScore operand1, WordComboScore operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(WordComboScore operand1, WordComboScore operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        public class List : List<WordComboScore>
        {
            public WordScorerBase WordScorer
            {
                get;
            }

            public List(WordScorerBase wordScorer)
            {
                WordScorer = wordScorer;
            }

            public string GetResults()
            {
                var runningScore = -1;
                var rank = 1;

                var text = new StringBuilder();
                foreach (var wordComboScore in this)
                {
                    var words = new StringBuilder();
                    foreach (var word in wordComboScore.WordCombo.Words)
                    {
                        words.Append(word);
                        words.Append(' ');
                    }

                    if (runningScore != -1 &&
                        wordComboScore.Score != runningScore)
                    {
                        text.Append(Environment.NewLine);
                        rank++;
                    }
                    runningScore = wordComboScore.Score;

                    // word combos
                    text.Append(words);

                    // score
                    text.Append(wordComboScore.Score);
                    
                    // rank
                    text.Append(" (");
                    text.Append(rank);
                    text.Append(rank.ToOrdinalSuffix());
                    text.Append(')');

                    // newline to seperate by rank
                    text.Append(Environment.NewLine);
                }

                return text.ToString();
            }
        }
    }
}
