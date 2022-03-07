using System;
using System.Collections.Generic;
using System.Text;

namespace WordListLib
{
    class ContainsSomewhere : WordListFilter
    {
        public string Filter { get; }
        public char Letter {  get; }

        public ContainsSomewhere(string filter, char letter)
        {
            Letter = letter;
        }

        protected override bool IsWordIncluded(string word)
        {
            for (int i = 0; i < Filter.Length; i++)
            {
                if (Filter[i] == '*')
                {
                    if (word[i] == Letter)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
