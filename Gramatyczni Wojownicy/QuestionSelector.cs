using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Klasa Statyczna. Przechowuje informacje o pytaniach, losuje nowe pytania z pliku oraz sprawdza poprawność odpowiedzi.
    /// </summary>
    public static class QuestionSelector
    {
        /// <summary>
        /// Wylosowane słowo.
        /// </summary>
        private static String _word;
        /// <summary>
        /// Wylosowane słowo z lukami.
        /// </summary>
        public static String wordToDisplay;
        /// <summary>
        /// Podpowiedź do słowa z lukami.
        /// </summary>
        public static String helpWord;
        /// <summary>
        /// Lista luk w słowie.
        /// </summary>
        public static List<String> gaps = new List<String>();
        /// <summary>
        /// Ilośc linijek w pliku ze słowami.
        /// </summary>
        private static int _numberOfLines = 0;
        /// <summary>
        /// Random
        /// </summary>
        static Random random = new Random();

        /// <summary>
        /// Szablon symbolu luki.
        /// </summary>
        private static String _gapReplacement = " ? ";



        /// <summary>
        /// Zwraca ilość linijek w pliku ze słowami
        /// </summary>
        /// <value>
        /// Ilość linijek w pliku.
        /// </value>
        public static int NumberOfLines
        {
            set { _numberOfLines = value; }
        }



        /// <summary>
        /// Losuje z pliku nowe słowo.
        /// </summary>
        private static void GetNewWord()
        {
            int counter = 0;
            String line = "";
            String path = @"files\words.txt";
            int chosenLine = random.Next(_numberOfLines);

            if (!File.Exists(path))
            {
                MessageBox.Show("Brak odowiedniego pliku tekstowego!");
            }
            else
            {
                StreamReader file = new StreamReader(path);

                while((line = file.ReadLine()) != null && counter != chosenLine)
                {
                    counter++;
                }

                _word = line;

            }
        }

        /// <summary>
        /// Zastępuje znaki h,ch,ż,rz,u,ó lukami i tworzy tekst wyświetlany jako pytanie.
        /// </summary>
        private static void GenerateWordToDisplay()
        {
            gaps.Clear();
            wordToDisplay = _word;
            String word = _word + " "; //bug fix

            for (int i = 0; i < _word.Length; i++)
            {

                if (word[i] == 'h' || word[i] == 'c' || word[i] == 'ż' || word[i] == 'r' || word[i] == 'u' || word[i] == 'ó')
                {
                    if (word[i] == 'h' || (word[i] == 'c' && word[i + 1] == 'h') || word[i] == 'ż' || (word[i] == 'r' && word[i + 1] == 'z') || word[i] == 'u' || word[i] == 'ó')
                    {
                        if (word[i] == 'c' || word[i] == 'r')
                        {
                            gaps.Add(word[i].ToString() + word[i + 1].ToString());
                            wordToDisplay = wordToDisplay.Replace(gaps[gaps.Count - 1], _gapReplacement);
                            i++;
                        }
                        else
                        {
                            gaps.Add(word[i].ToString());
                            wordToDisplay = wordToDisplay.Replace(gaps[gaps.Count - 1], _gapReplacement);
                        }
                    }
                }

            }

        }

        /// <summary>
        /// Tworzy tekst z podpowiedzią wyświetlaną w trakcie gry.
        /// </summary>
        private static void GenerateHelpWord()
        {
            bool helped = false;
            String word = _word + " "; //bug fix
            helpWord = "";
            for (int i = 0; i < _word.Length; i++)
            {
                if(word[i] == 'ż' || (word[i] == 'r' && word[i+1] == 'z'))
                {
                    if(word[i] == 'r') { i++; }
                    {
                        if (helped)
                        {
                            if (helpWord[i - 1] != ' ') { helpWord += " "; }
                        }
                    }
                    helpWord += "ż/rz";
                    helped = true;
                }
                else if(word[i] == 'h' || (word[i] == 'c' && word[i+1] == 'h'))
                {
                    if (word[i] == 'c') { i++; }
                    {
                        if (helped)
                        {
                            if (helpWord[i - 1] != ' ') { helpWord += " "; }
                        }
                    }
                        
                    helpWord += "h/ch";
                    helped = true;
                }
                else if(word[i] == 'u' || word[i] == 'ó')
                {
                    if (helped)
                    {
                        if (helpWord[i - 1] != ' ') { helpWord += " "; }
                    }
                    helpWord += "u/ó";
                    helped = true;
                }
                else
                {
                    helpWord += " ";
                }
            }
        }

        /// <summary>
        /// Zadaje nowe pytanie i ustawia jego parametry. (Korzysta z metod GetNewWord(), GenerateWordToDisplay() i GenerateHelpWord())
        /// </summary>
        public static void AskQuestion()
        {
            GetNewWord();
            GenerateWordToDisplay();
            GenerateHelpWord();
            
        }

        /// <summary>
        /// Zwraca prawdę, gdy lista z lukami do odgadnięcia jest pusta.
        /// </summary>
        /// <value>
        ///   <c>true</c>Gdy lista z lukami jest pusta; w innym przypadku, <c>false</c>.
        /// </value>
        public static bool IsGapsListEmpty
        {
            get
            {
                return !gaps.Any<String>();
            }
        }

        /// <summary>
        /// Wykonuje się w momencie gdy zostanie poprawnie uzupełniona luka. Zamienia lukę na słowo i usuwa zużytą lukę z listy.
        /// </summary>
        public static void NextGap()
        {
            if (!IsGapsListEmpty)
            {
                for (int i = 0; i < wordToDisplay.Length; i++)
                {
                    if (wordToDisplay[i] == _gapReplacement[0] && wordToDisplay[i+1] == _gapReplacement[1] && wordToDisplay[i+2] == _gapReplacement[2])
                    {
                        wordToDisplay = wordToDisplay.Remove(i, _gapReplacement.Length);
                        wordToDisplay = wordToDisplay.Insert(i, gaps[0]);
                        gaps.RemoveAt(0);
                        break;
                    }
                }
            }
        }
    }
}
