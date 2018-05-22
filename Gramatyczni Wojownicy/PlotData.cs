using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.IO;
using System.Windows;

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Klasa z danymi statystyk
    /// </summary>
    public class PlotData
    {
        /// <summary>
        /// Tytuł wykresu.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Lista z punktami na wykresie.
        /// </summary>
        public List<DataPoint> Points { get; private set; }

        /// <summary>
        /// Konstruktor klasy PlotData <see cref="PlotData"/> class.
        /// </summary>
        public PlotData()
        {
            this.Title = "";
            Points = new List<DataPoint>();
            GetScores();   
        }


        /// <summary>
        /// Pobiera procentowe wyniki z pliku zalogowanego użytkownika i dodaje je do listy z punktami wykresu
        /// </summary>
        public void GetScores()
        {
            int counter = 0;
            String line = "";
            String path = StatisticsCollector.UserFile;

            if (!File.Exists(path))
            {
                MessageBox.Show(StatisticsCollector.UserFile);
            }
            else
            {
                StreamReader file = new StreamReader(path);

                while ((line = file.ReadLine()) != null)
                {
                    DataPoint point = new DataPoint((double)counter, Convert.ToDouble(line));
                    Points.Add(point);
                    counter++;
                }

            }
        }
    }
}

