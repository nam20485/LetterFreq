using System;
using System.Collections.Generic;
using System.Text;

namespace WordListLib
{
    public class LengthIs : WordListFilter
    {
        public int Length {  get; }

        public LengthIs(int length)
        {
            Length = length;
        }

        protected override bool IsWordIncluded(string word)
        {
            return word.Length == Length;            
        }
    }
}
