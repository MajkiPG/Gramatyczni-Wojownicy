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

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Okno instrukcji
    /// </summary>
    public partial class HelpWindow : Window
    {
        /// <summary>
        /// Konstruktor okna instrukcji<see cref="HelpWindow"/> class.
        /// </summary>
        public HelpWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Dekorator przycisku. Usuwa tło.
        /// </summary>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            Color color = Colors.IndianRed;
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
        /// Zamyka okno instrukcji.
        /// </summary>
        private void CloseHelpButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)this.DataContext;
            mainWindow.loggedUserLabel.Content = StatisticsCollector.loggedUser;
            mainWindow.DisplayMenu();
            this.Close();
        }
    }
}
