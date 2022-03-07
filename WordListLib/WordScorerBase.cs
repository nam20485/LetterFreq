using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordListLib
{
    public abstract class WordScorerBase
    {               
        public WordList.LetterScoresMap LetterScores
        {
            get;
            protected set;
        }

        public WordCombo.List WordComboList
        {
            get;
            private set;
        }

        public abstract string ScorerDescription
        {
            get;
        }

        //public WordScorerBase(Word)
        //{
        //    LetterScores = letterScores;
        //}

        internal WordComboScore.List ScoreWordList(WordCombo.List wordComboList)
        {           
            WordComboList = wordComboList;

            Initialize();

            var wordComboScores = new WordComboScore.List(this);

            foreach (var wordCombo in wordComboList)
            {
                wordComboScores.Add(new WordComboScore(wordCombo, ScoreWordCombo(wordCombo)));
            }

            return wordComboScores;
        }        

        protected int ScoreWordCombo(WordCombo words)
        {
            var score = 0;

            var scoredLetters = new List<char>();

            foreach (var word in words.Words)
            {
                score += ScoreWord(word, ref scoredLetters);
            }

            return score;
        }

        protected virtual int ScoreWord(string word, ref List<char> scoredLetters)
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

        protected virtual void Initialize()
        {

        }
    }
}
