using System;
using System.Collections.Generic;
using System.Text;

using static WordListLib.WordList;

namespace WordListLib
{
    public class LetterOccurrenceInAnswersWordScorer : WordScorerBase
    {
        public override string ScorerDescription => "LetterOccurrenceInAnswersWordScorer";

        protected override void Initialize()
        {
            LetterScores = CountLetterOccurrences(WordComboList.OriginalList);
        }
      
        public static LetterScoresMap CountLetterOccurrences(WordList words)
        {
            var letterOccurences = new LetterScoresMap();

            int occurrences = 0;
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                foreach (var word in words)
                {
                    if (word.Contains(letter))
                    {
                        occurrences++;
                    }
                }

                letterOccurences[letter] = occurrences;
            }

            return letterOccurences;
        }
    }
}
