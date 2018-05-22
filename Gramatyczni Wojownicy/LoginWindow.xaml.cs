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
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Okno logowania i funkcje z nim związane
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Konstruktor okna logowania. <see cref="LoginWindow"/> class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            StatisticsCollector.GetUsers();
            usersListBox.ItemsSource = StatisticsCollector.users;
        }

        /// <summary>
        /// Loguje użytkownika wybranego z listy.
        /// </summary>
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsCollector.LogIn(usersListBox.SelectedItem.ToString());
            UpdateLoggedUserLabel();
            this.Close();
        }

        /// <summary>
        /// Wywołana przy zaznaczeniu loginu z listy. Aktywuje przycisk logowania.
        /// </summary>
        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(usersListBox.SelectedValue == null)
            {
                logInButton.Opacity = 30;
            }
            else
            {
                logInButton.Opacity = 100;
            }
        }

        /// <summary>
        /// Wyświetla formularz tworzenia nowego użytkownika.
        /// </summary>
        private void NewUserButton_MouseDown(object sender, RoutedEventArgs e)
        {
            newUserTextBox.Visibility = Visibility.Visible;
            createUserButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Pobiera nazwę nowego użytkownika z pola tekstowego. Sprawdza poprawnośc i wywołuje fukcje tworzące nowego użytkownika.
        /// </summary>
        private void CreateUserButton_MouseDown(object sender, RoutedEventArgs e)
        {
            if (newUserTextBox.Text == "" || newUserTextBox.Text == "Tu wpisz login" || StatisticsCollector.users.Exists(x => String.Equals(x, newUserTextBox.Text, StringComparison.OrdinalIgnoreCase) || newUserTextBox.Text.Length > 10))
            {
                MessageBox.Show("Nazwa jest już zajęta lub niezgodna! Wybierz inną. Maks 10 znaków!");
            }
            else
            {
                StatisticsCollector.CreateNewUser(newUserTextBox.Text);
                UpdateLoggedUserLabel();

                this.Close();
            }
        }

        /// <summary>
        /// Aktualizuje informację o zalogowanym użytkowniku w klasie MainWindow.
        /// </summary>
        private void UpdateLoggedUserLabel()
        {
            MainWindow mainWindow = (MainWindow)this.DataContext;
            mainWindow.loggedUserLabel.Content = StatisticsCollector.loggedUser;
            mainWindow.DisplayMenu();
        }

        /// <summary>
        /// Dekorator przycisku. Ustawia tło.
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

    }
}
