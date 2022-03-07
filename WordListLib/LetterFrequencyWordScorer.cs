using System;
using System.Collections.Generic;
using System.Text;

using static WordListLib.WordList;

namespace WordListLib
{
    public class LetterFrequencyWordScorer : WordScorerBase
    {
        public override string ScorerDescription => "LetterFrequencyWordScorer";

        protected override void Initialize()
        {
            LetterScores = CountLetterFrequencies(WordComboList.OriginalList);
        }        

        public static LetterScoresMap CountLetterFrequencies(WordList words)
        {
            var letterFrequencies = new LetterScoresMap();

            foreach (var word in words)
            {
                var lowerCaseWord = word.ToLower();
                foreach (var letter in lowerCaseWord)
                {
                    if (char.IsLetter(letter))
                    {
                        if (letterFrequencies.ContainsKey(letter))
                        {
                            letterFrequencies[letter] += 1;
                        }
                        else
                        {
                            letterFrequencies[letter] = 1;
                        }
                    }
                }
            }

            return letterFrequencies;
        }        

        protected override int ScoreWord(string word, ref List<char> scoredLetters)
        {
            var score = 0;            

            var lowerCaseWord = word.ToLower();

            foreach (var letter in lowerCaseWord)
            {
                if (char.IsLetter(letter))
                {
                    if (!scoredLetters.Contains(letter))
                    {
                        score += LetterScores[letter];
                        scoredLetters.Add(letter);
                    }
                }
            }

            return score;
        }
    }
}
