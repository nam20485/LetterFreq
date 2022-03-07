using System;
using System.Collections.Generic;
using System.Text;

namespace WordListLib
{
    public abstract class WordListFilter
    {
        public WordList Apply(WordList wordList)
        {
            var filtered = new WordList();
            foreach (var word in wordList)
            {
                if (IsWordIncluded(word))
                {
                    filtered.Add(word);
                }
            }
            return filtered;
        }

        protected abstract bool IsWordIncluded(string word);
    }
}
