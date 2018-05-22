using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Klasa statyczna. Przechowuje informacje o użytkownikach, zajmuje się punktacją i zapisem wyników do pliku.
    /// </summary>
    static public class StatisticsCollector
    {

        /// <summary>
        /// Szablon użytkownika gość.
        /// </summary>
        public static String guestUser = "Gość";
        /// <summary>
        /// Zalogowany użytkownik.
        /// </summary>
        public static String loggedUser = guestUser;
        /// <summary>
        /// Lista użytkowników.
        /// </summary>
        public static List<String> users = new List<String>();
        /// <summary>
        /// Błędne odpowiedzi
        /// </summary>
        private static int _correctAnswers = 0;
        /// <summary>
        /// Poprawne odpowiedzi.
        /// </summary>
        private static int _wrongAnswers = 0;

        /// <summary>
        /// Lokalizacja folderu z użytkownikami.
        /// </summary>
        private static String _usersDirectory = @"users\";
        /// <summary>
        /// Lokalizacja pliku zalogowanego użytkownika.
        /// </summary>
        private static String _userFile;


        /// <summary>
        /// Pobiera lokalizacje pliku użytkownika.
        /// </summary>
        /// <value>
        /// _userFile
        /// </value>
        public static String UserFile
        {
            get { return _userFile; }
        }


        /// <summary>
        /// Pobiera listę użytkowników z folderu /Users.
        /// </summary>
        public static void GetUsers()
        {
            users.Clear();
            String[] userTxtFiles = Directory.GetFiles(_usersDirectory, "*.txt");
            users.AddRange(userTxtFiles);

            for (int i = 0; i < users.Count; i++)
            {
                users[i] = users[i].Replace(_usersDirectory, "").Replace(".txt", "");
            }
        }

        /// <summary>
        /// Loguje danego użytkownika.
        /// </summary>
        /// <param name="username">Nazwa użytkownika logującego się.</param>
        public static void LogIn(String username)
        {
            loggedUser = username;
            _userFile = _usersDirectory + loggedUser + ".txt";
        }

        /// <summary>
        /// Tworzy nowego użytkownika i dodaje jego plik do folderu /users.
        /// </summary>
        /// <param name="username">Nazwa nowego użytkownika.</param>
        public static void CreateNewUser(String username)
        {
            FileInfo newUserFile = new FileInfo(_usersDirectory + username + ".txt");

            if(!newUserFile.Exists)
            {
                using (StreamWriter sw = newUserFile.CreateText())
                {
                    sw.WriteLine("0");
                }
            }

            LogIn(username);
        }

        /// <summary>
        /// Punktuje poprawną lub niepoprawną odpowiedź.
        /// </summary>
        /// <param name="answer">Poprawna(true) lub niepoprawna(false) odpowiedź.</param>
        public static void RecievePoints(bool answer)
        {
            if (answer)
            {
                _correctAnswers++;
            }
            else
            {
                _wrongAnswers++;
            }
        }

        /// <summary>
        /// Wylicza punktację na podpstawie różnicy poprawnych i niepoprawnych odpowiedz.
        /// </summary>
        /// <returns>Zwraca liczbę punktów.</returns>
        public static int Score()
        {
            return _correctAnswers - _wrongAnswers;
        }

        /// <summary>
        /// Zapisuje procentowy wynik gry do pliku zalogowanego użytkownika.
        /// </summary>
        public static void SaveScoreInFile()
        {
            if (loggedUser != guestUser)
            {
                double percentage = ((double)_correctAnswers / ((double)_correctAnswers + (double)_wrongAnswers)) * 100;

                using (StreamWriter sw = File.AppendText(_userFile))
                {
                    sw.WriteLine(percentage.ToString());
                }
            }

            ResetScores();
        }

        /// <summary>
        /// Resetuje wyniki.
        /// </summary>
        public static void ResetScores()
        {
            _correctAnswers = 0;
            _wrongAnswers = 0;
        }

        /// <summary>
        /// Wylogowuje zalogowanego użytkownika. (Automatycznie loguje użytkownika Gość)
        /// </summary>
        public static void LogOut()
        {
            ResetScores();
            loggedUser = guestUser;
            _userFile = "";
        }

        
    }
}
