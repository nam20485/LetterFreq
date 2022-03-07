using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordListLib;
using System.Text.Json;
using System.IO;
using System.Text;

namespace LetterFreq
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var allFiveLetterWords = allWords.Filter(new LengthIs(5));          
            //var wordScoresMap = allFiveLetterWords.ScoreWords(new LetterFrequencyWordScorer());
            //wordScoresMap.Sort();

            var wordleAnswers = new WordList(@"..\..\..\wordle-answers.txt");            
            var letterFrequencies = wordleAnswers.LetterFrequencies;
            var wordleAnswerTwoWordCombinations = WordCombo.List.Make(wordleAnswers);
            //var wordComboScores = wordleAnswerTwoWordCombinations.Score(new LetterFrequencyWordScorer());
            //var wordComboScores = wordleAnswerTwoWordCombinations.Score(new LetterOccurrenceInAnswersWordScorer());

            var wordScorers = new WordScorerBase[]
            {
                new LetterOccurrenceInAnswersWordScorer()
                ,new LetterFrequencyWordScorer()
                //,new MatchingLetterPositionsWordScorer()
            };

            foreach (var wordScorer in wordScorers)
            {
                var wordComboScores = wordleAnswerTwoWordCombinations.Score(wordScorer);
                wordComboScores.Sort();
                wordComboScores.Reverse();

                File.WriteAllText($"wordle-answers-two-word-combo-scores_{wordComboScores.WordScorer.ScorerDescription}.txt", wordComboScores.GetResults());
            }           

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
