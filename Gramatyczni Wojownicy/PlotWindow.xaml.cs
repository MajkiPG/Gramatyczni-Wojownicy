using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot;

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Okno wyświetlające wykres statystyk. Korzysta z bibliotek OxyPlot.
    /// </summary>
    public partial class PlotWindow : Window
    {
        /// <summary>
        /// Konstruktor okna statystyk<see cref="PlotWindow"/> class.
        /// </summary>
        public PlotWindow()
        {
            InitializeComponent();
            userPlotLabel.Content = "Statystyki użytkownika " + StatisticsCollector.loggedUser;
            
        }



        /// <summary>
        /// Dekorator przycisku. Usuwa tło.
        /// </summary>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            Color color = Colors.CadetBlue;
            color.A = 60;
            label.Background = new SolidColorBrush(color);
        }

        /// <summary>
        /// Dekorator przycisku. Usuwa tło.
        /// </summary>
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Background = System.Windows.Media.Brushes.Transparent;
        }

        /// <summary>
        /// Zamyka okno statystyk.
        /// </summary>
        private void ClosePlotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
