using System;
using System.Collections.Generic;
using System.Text;

namespace WordListLib
{
    class ContainsSpecifically : WordListFilter
    {
        public string Filter {  get; }

        public ContainsSpecifically(string filter)
        {
            Filter = filter;
        }

        protected override bool IsWordIncluded(string word)
        {
            var lowerCaseFilter = Filter.ToLower();

            for (int i = 0; i < Filter.Length; i++)
            {
                if (Filter[i] != '*')
                {
                    if (word.ToLower()[i] != lowerCaseFilter[i])
                    {
                       return false;
                    }
                }
            }

            return true;
        }
    }
}
