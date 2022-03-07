using System;
using System.Collections.Generic;
using System.Text;

namespace WordListLib
{
    public class MatchingLetterPositionsWordScorer : WordScorerBase
    {
        public override string ScorerDescription => "MatchingLetterPositionsWordScorer";

        protected override int ScoreWord(string word, ref List<char> scoredLetters)
        {
            int score = 0;

            foreach (var answer in WordComboList.OriginalList)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == answer[i])
                    {
                        score++;
                    }
                }
            }           

            return score;
        }
    }
}
