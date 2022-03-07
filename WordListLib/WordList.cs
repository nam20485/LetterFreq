using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordListLib
{
    public class WordList : List<string>
    {
        public string Path { get; }

        public class LetterScoresMap : Dictionary<char, int>
        {
        }

        public int WordLength
        {
            get;
        } = -1;

        public WordList()
            : base()
        {
        }

        public WordList(int length)
            : base()
        {
            WordLength = length;
        }

        public WordList(IEnumerable<string> words)
            : base(words)
        {            
        }   
        
        public WordList(string path)
        {
            Path = path;
            AddRange(ReadWordsFromFile());
        }

        private List<string> ReadWordsFromFile()
        {
            return File.ReadAllLines(Path).ToList();
        }

        public WordList Filter(string filter)
        {
            if (filter.Length == WordLength)
            {
                var filtered = new WordList(WordLength);

                var lowerCaseFilter = filter.ToLower();

                foreach (var word in this)
                {
                    bool matches = true;

                    for (int i = 0; i < filter.Length; i++)
                    {
                        if (filter[i] != '*')
                        {
                            if (word.ToLower()[i] != lowerCaseFilter[i])
                            {
                                matches = false;
                                break;
                            }
                        }
                    }

                    if (matches)
                    {
                        filtered.Add(word);
                    }
                }               

                return filtered;
            }

            // no filtering
            return this;
        }

        public WordList Filter(int length)
        {
            var filtered = new WordList(length);
            foreach (var word in this)
            {
                if (word.Length == length)
                {
                    filtered.Add(word);
                }
            }
            return filtered;
        }

        public WordList Filter(WordListFilter filter)
        {
            return filter.Apply(this);
        }

        public class LetterFrequencyMap : Dictionary<char, int>
        {

        }

        private LetterScoresMap _letterFrequencies;

        public LetterScoresMap LetterFrequencies
        {
            get
            { 
                if (_letterFrequencies == null)
                {
                    _letterFrequencies = LetterFrequencyWordScorer.CountLetterFrequencies(this);
                }
                return _letterFrequencies;
            }
        }       
    }
}
