using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Gramatyczni Wojownicy
/// </summary>
namespace Gramatyczni_Wojownicy
{
    /// <summary>
    /// Główne okno aplikacji
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Początkowa wysokość grafiki eksplozji.
        /// </summary>
        int explosionInitialHeight = 100;
        /// <summary>
        /// Indykuje czy rakieta jest zakotwiczona do kursora.
        /// </summary>
        bool rocketCaptured = false;
        /// <summary>
        /// Czy odtwarzany jest dźwięk silnika rakietowego.
        /// </summary>
        bool isRocketThrustersSound = false;
        /// <summary>
        /// Czy odtwarzany jest dźwięk silnika.
        /// </summary>
        bool isRocketSteamSound = false;
        /// <summary>
        /// Indykuje czy gra jest uruchomiona.
        /// </summary>
        bool gameStarted = false;
        /// <summary>
        /// Odlicza do kolejnej zmiany rozmiaru pola wybuchu rakiety.
        /// </summary>
        int rocketExplosionCounter = 0;
        /// <summary>
        /// Odlicza do kolejnej zmiany kąta rakiety.
        /// </summary>
        int rocketAngleCounter = 0;
        /// <summary>
        /// Liczy liczbę asteroid;
        /// </summary>
        int AsteroidsCounter = 0;
        /// <summary>
        /// Odlicza do kolejnej zmiany ustawienia płomienia rakiety.
        /// </summary>
        int rocketFlameCounter = 0;
        /// <summary>
        /// The random
        /// </summary>
        Random random = new Random();
        /// <summary>
        /// Punkt pomocniczy do wyliczenia kąta rakiety.
        /// </summary>
        Point startPoint;

        /// <summary>
        /// Lista aktywnych animacji asteroidy.
        /// </summary>
        List<Storyboard> asteroidAnimationList = new List<Storyboard>();
        /// <summary>
        /// Lista asteroid.
        /// </summary>
        List<ContentControl> asteroids = new List<ContentControl>();

        /// <summary>
        /// Odlicza czas między dodaniem kolejnych asteroid.
        /// </summary>
        DispatcherTimer startAsteroidsTimer = new DispatcherTimer();
        /// <summary>
        /// Odlicza czas trwania animacji eksplozji.
        /// </summary>
        DispatcherTimer explosionTimer = new DispatcherTimer();
        /// <summary>
        /// Odlicza czas pojawienia się rakiety po eksplozji.
        /// </summary>
        DispatcherTimer regenerateRocketTimer = new DispatcherTimer();
        /// <summary>
        /// Odlicza czas do zmiany ustawienia płomienia rakiety.
        /// </summary>
        DispatcherTimer rocketFlameTimer = new DispatcherTimer();
        /// <summary>
        /// Odlicza opóźnienie pojawienia się kolejnego słowa.
        /// </summary>
        DispatcherTimer wordLabelDelayTimer = new DispatcherTimer();
        /// <summary>
        /// Odlicza czas pokazywania się podpowiedzi.
        /// </summary>
        DispatcherTimer helpWordDisplayTimer = new DispatcherTimer();

        /// <summary>
        /// Dźwięk eksplozji.
        /// </summary>
        SoundPlayer explosion = new SoundPlayer(@"files\sounds\sonic_explosion.wav");
        /// <summary>
        /// Dźwięk włączonego silnika rakiety.
        /// </summary>
        SoundPlayer rocketSteam = new SoundPlayer(@"files\sounds\rocket_steam_low.wav");
        /// <summary>
        /// Dźwięk płomienia rakiety.
        /// </summary>
        SoundPlayer rocketThrusters = new SoundPlayer(@"files\sounds\rocket_thrusters.wav");


        /// <summary>
        /// Konstruktor głównego okna aplikacji.<see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            QuestionSelector.NumberOfLines = File.ReadAllLines(@"files\words.txt").Length;
            DisplayMenu();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.PreviewKeyDown += new KeyEventHandler(HandleH);

            startAsteroidsTimer.Tick += StartAsteroidsTimer_Tick;
            startAsteroidsTimer.Interval = TimeSpan.FromSeconds(1);

            explosionTimer.Tick += ExplosionTimer_Tick;
            explosionTimer.Interval = TimeSpan.FromSeconds(.01);

            regenerateRocketTimer.Tick += RegenerateRocketTimer_Tick;
            regenerateRocketTimer.Interval = TimeSpan.FromSeconds(1);

            rocketFlameTimer.Tick += RocketFlameTimer_Tick;
            rocketFlameTimer.Interval = TimeSpan.FromSeconds(.1);

            wordLabelDelayTimer.Tick += WordLabelDelayTimer_Tick;
            wordLabelDelayTimer.Interval = TimeSpan.FromSeconds(1);

            helpWordDisplayTimer.Tick += HelpWordDisplayTimer_Tick;
            helpWordDisplayTimer.Interval = TimeSpan.FromSeconds(2);
        }


        /// <summary>
        /// Występuje przy wciśnięciu ESC. Pauzuję grę.
        /// </summary>       
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ReleaseRocket();
                PauseAsteroids();
                DisplayMenu();
                startAsteroidsTimer.Stop();
            }
        }

        /// <summary>
        /// Występuje przy wciśnięciu h. Wyświetla podpowiedź.
        /// </summary>
        private void HandleH(object sender, KeyEventArgs e)
        {
            if (gameStarted)
            {
                if (e.Key == Key.H)
                {
                    helpLabel.Content = QuestionSelector.helpWord;
                    helpLabel.Visibility = Visibility.Visible;
                    helpWordDisplayTimer.Start();
                }
            }
        }

        /// <summary>
        /// Występuje przy poruszeniu kursorem po planszy. Gdy rakieta jest zakotwiczona, to ja przemieszcza zgodnie z kursorem.
        /// </summary>
        private void PlayArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (rocketCaptured)
            {
                Point relativePosition = e.GetPosition(playArea);
                rocketFlameCounter++;


                Canvas.SetLeft(rocket, relativePosition.X - rocket.ActualWidth / 2);
                Canvas.SetTop(rocket, relativePosition.Y - rocket.ActualHeight / 2);

                rocketAngleCounter++;


                if (rocketAngleCounter == 1)
                {
                    startPoint = relativePosition;
                }
                else if (rocketAngleCounter == 20)
                {
                    rocket.LayoutTransform = new RotateTransform(RocketAngle(startPoint, relativePosition));
                    rocketAngleCounter = 0;
                }
            }
        }

        /// <summary>
        /// Występuje gdy kursor opuszcza planszę. Gdy rakieta jest zakotwiczona to wywołuje funkcję DestroYRocket();
        /// </summary>
        private void PlayArea_MouseLeave(object sender, MouseEventArgs e)
        {
            if (rocketCaptured)
            {
                DestroyRocket();
            }
        }

        /// <summary>
        /// Zakotwicza rakietę do kursora. 
        /// </summary>
        private void Rocket_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!rocketCaptured)
            {
                CaptureRocket();
                if(gameStarted) { DisplayGameControls(); }
                startAsteroidsTimer.Start();
            }
        }

        /// <summary>
        /// Występuje gdy gracz najedzie rakietą na dobrą odpowiedź. Wywołuje funkcje przygotowujące następne pytanie.
        /// </summary>
        private void CorrectPlanet_MouseEnter(object sender, MouseEventArgs e)
        {
            if (rocketCaptured)
            {
                QuestionSelector.NextGap();
                StatisticsCollector.RecievePoints(true);

                if (QuestionSelector.IsGapsListEmpty)
                {
                    SetQuestionLabel();
                    correctPlanet.Visibility = Visibility.Collapsed;
                    falsePlanet.Visibility = Visibility.Collapsed;

                    wordLabelDelayTimer.Start();

                }
                else
                {
                    SetQuestionLabel();
                    SetPlanetPosition();
                    SetPlanetContent();
                }

            }

            UpdateScoreLabel();
        }

        /// <summary>
        /// Występuje gdy gracz najedzie rakietą na złą odpowiedź. Wywołuje funkcję związane ze złą odpowiedzią.
        /// </summary>
        private void FalsePlanet_MouseEnter(object sender, MouseEventArgs e)
        {
            if (rocketCaptured)
            {
                StatisticsCollector.RecievePoints(false);
                falsePlanet.Visibility = Visibility.Collapsed;
                DestroyRocket();
                UpdateScoreLabel();
            }
        }

        /// <summary>
        /// Rozpoczyna grę. Wywołuje wszystkie funkcje związane z przygotowaniem nowej rozgrywki.
        /// </summary>
        private void StartButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gameStarted = true;
            NextQuestion();
            SetPlanetPosition();
            SetRocketPosition();
            DisplayGameControls();
            UpdateScoreLabel();
            startAsteroidsTimer.Start();
        }

        /// <summary>
        /// Otwiera okno logowania użytkownika.
        /// </summary>
        private void LogIn_Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DataContext = this;
            loginWindow.Show();
        }

        /// <summary>
        /// Wywołuje funkcje związane z wylogowaniem użytkownika.
        /// </summary>
        private void LogOutButton_Down(object sender, MouseButtonEventArgs e)
        {
            StatisticsCollector.LogOut();
            loggedUserLabel.Content = StatisticsCollector.loggedUser;
            DisplayMenu();
        }

        /// <summary>
        /// Kończy grę. Wywołuje funkcje kończące grę i zapisujące wyniki.
        /// </summary>
        private void EndGameButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gameStarted = false;
            AsteroidsCounter = 0;
            StatisticsCollector.SaveScoreInFile();
            RemoveAsteroids();
            DisplayMenu();
            
        }

        /// <summary>
        /// Występuje gdy użytkownik najedzie rakietą na asteroidę. Wywołuje funkjcę DestroyRocket().
        /// </summary>
        private void Asteroid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (rocketCaptured)
            {
                DestroyRocket();
            }
        }

        /// <summary>
        /// Zamyka wszystkie elementy programu przy wcześniejszym zapisaniu wyniku zalogowanego użytkownika.
        /// </summary>
        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(gameStarted) { StatisticsCollector.SaveScoreInFile(); }
            Environment.Exit(0);
        }

        /// <summary>
        /// Dekorator przycisku. Usuwa tło.
        /// </summary>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            Color color = Colors.HotPink;
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
        /// Wyświetla okno ze statystykami zalogowanego użytkownika.
        /// </summary>
        private void StatisticsButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (StatisticsCollector.loggedUser != StatisticsCollector.guestUser)
            {
                PlotWindow plotWindow = new PlotWindow();
                plotWindow.Show();
            }
        }

        /// <summary>
        /// Wyświetla okno ze statystykami zalogowanego użytkownika.
        /// </summary>
        private void HowToPlayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            logo.Visibility = Visibility.Collapsed;
            HelpWindow helpWinodw = new HelpWindow();
            helpWinodw.DataContext = this;
            helpWinodw.Show();
        }

        /// <summary>
        /// Timer odpowiadający za dodawanie asteroid w odstępach czasu po starcie gry.
        /// </summary>
        private void StartAsteroidsTimer_Tick(object sender, EventArgs e)
        {
            if (AsteroidsCounter >= 8)
            {
                startAsteroidsTimer.Stop();
            }
            else
            {
                AddAsteroid();
                AsteroidsCounter++;
            }
        }

        /// <summary>
        /// Timer wspomagający animację wybuchu rakiety.
        /// </summary>
        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            rocketExplosion.Height = explosionInitialHeight;
            rocketExplosionCounter++;
            explosionInitialHeight += 3;


            if (rocketExplosionCounter == 20)
            {
                explosionTimer.Stop();
                rocket.Visibility = Visibility.Hidden;
                rocketExplosion.Visibility = Visibility.Hidden;
                rocketExplosionCounter = 0;
                explosionInitialHeight = 100;
                regenerateRocketTimer.Start();
                if (gameStarted) { SetPlanetPosition(); }
            }
        }

        /// <summary>
        /// Timer odliczający czas do pojawienia się rakiety na planszy po wywołaniu funkcji DestroyRocket().
        /// </summary>
        private void RegenerateRocketTimer_Tick(object sender, EventArgs e)
        {
            SetRocketPosition();
            regenerateRocketTimer.Stop();
        }

        /// <summary>
        /// Timer wspomagający animację płomienia rakiety.
        /// </summary>
        private void RocketFlameTimer_Tick(object sender, EventArgs e)
        {
            if (rocketFlameCounter > 40)
            {
                rocketFlame.Visibility = Visibility.Visible;
                rocketFlameCounter = 0;
                isRocketSteamSound = false;

                if (!isRocketThrustersSound)
                {
                    isRocketThrustersSound = true; ;
                    rocketThrusters.PlayLooping();
                }
            }
            else
            {
                rocketFlame.Visibility = Visibility.Collapsed;
                rocketFlameCounter = 0;
                isRocketThrustersSound = false;

                if (!isRocketSteamSound)
                {
                    rocketSteam.PlayLooping();
                    isRocketSteamSound = true;
                }
            }
        }

        /// <summary>
        /// Opóźnia wyświetlenie kolejnego pytania po odgadnięciu całego słowa.
        /// </summary>
        private void WordLabelDelayTimer_Tick(object sender, EventArgs e)
        {
            NextQuestion();
            wordLabelDelayTimer.Stop();
        }

        /// <summary>
        /// Timer wyświetlający podpowiedź przez ustalony czas.
        /// </summary>
        private void HelpWordDisplayTimer_Tick(object sender, EventArgs e)
        {
            helpLabel.Visibility = Visibility.Collapsed;
            helpWordDisplayTimer.Stop();
        }



        /// <summary>
        /// Zakotwicza rakietę do kursora.
        /// </summary>
        private void CaptureRocket()
        {
            rocketCaptured = true;
            rocket.IsHitTestVisible = false;
            playArea.Cursor = Cursors.None;
            smallRocketFlame.Visibility = Visibility.Visible;
            rocketSteam.PlayLooping();
            rocketFlameTimer.Start();
            StartAsteroids();
        }

        /// <summary>
        /// Odkotwicza rakietę od kursora.
        /// </summary>
        private void ReleaseRocket()
        {
            rocketCaptured = false;
            rocket.IsHitTestVisible = true;
            playArea.Cursor = Cursors.Arrow;
            rocketFlame.Visibility = Visibility.Collapsed;
            smallRocketFlame.Visibility = Visibility.Collapsed;
            rocketSteam.Stop();
            rocketThrusters.Stop();
            rocketFlameTimer.Stop();
        }

        /// <summary>
        /// Wylicza kąt pod jakim rakieta ma się ustawić na planszy na podstawie dwóch pozycji.
        /// </summary>
        /// <param name="position1">Pozycja początkowa</param>
        /// <param name="position2">Pozycja końcowa</param>
        /// <returns>Zwraca w stopniac kąt ustawienia rakiety.</returns>
        private double RocketAngle(Point position1, Point position2)
        {
            double angle;

            if (position1.X < position2.X)
            {
                if (position1.Y > position2.Y)
                {
                    return angle = Math.Atan((position2.X - position1.X) / (position1.Y - position2.Y)) * (180 / Math.PI);         //I QUARTER
                }
                else if (position1.Y < position2.Y)
                {
                    return angle = Math.Atan((position2.Y - position1.Y) / (position2.X - position1.X)) * (180 / Math.PI) + 89;         //IV QUARTER
                }
                else
                {
                    return angle = 90;
                }
            }
            else if (position1.X > position2.X)
            {
                if (position1.Y < position2.Y)
                {
                    return angle = Math.Atan((position1.X - position2.X) / (position2.Y - position1.Y)) * (180 / Math.PI) + 179;         //III QUARTER
                }
                else if (position1.Y > position2.Y)
                {
                    return angle = Math.Atan((position1.Y - position2.Y) / (position1.X - position2.X)) * (180 / Math.PI) + 269;         //II QUARTER
                }
                else
                {
                    return angle = 270;
                }
            }
            else
            {
                if (position1.Y > position2.Y)
                {
                    return angle = 0;
                }
                else
                {
                    return angle = 180;
                }
            }

        }

        /// <summary>
        /// Dodaje losową animację do danej instancji asteroidy.
        /// </summary>
        /// <param name="asteroid">Obiekt asteroidy.</param>
        /// <param name="from">Początek animacji na planszy.</param>
        /// <param name="to">Koniec animacji na planszy</param>
        /// <param name="propertyToAnimate">Właśnośc obiektu do animacji. (Płaszczyzna X lub Y)</param>
        private void AnimateAsteroid(ContentControl asteroid, double from, double to, PropertyPath propertyToAnimate)
        {
            Storyboard storyboard = new Storyboard() { AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };
            DoubleAnimation animation = new DoubleAnimation() { From = from, To = to, Duration = new Duration(TimeSpan.FromSeconds((random.Next(3) + 3))) };
            Storyboard.SetTarget(animation, asteroid);
            Storyboard.SetTargetProperty(animation, propertyToAnimate);
            storyboard.Children.Add(animation);
            asteroidAnimationList.Add(storyboard);
            storyboard.Begin();
        }

        /// <summary>
        /// Dodaje asteroidę wraz z animacją na planszę.
        /// </summary>
        private void AddAsteroid()
        {
            ContentControl asteroid = new ContentControl();
            asteroid.Template = Resources["AsteroidTemplate"] as ControlTemplate;
            AnimateAsteroid(asteroid, 0, playArea.ActualWidth - 100, new PropertyPath("(Canvas.Left)"));
            AnimateAsteroid(asteroid, random.Next((int)playArea.ActualHeight - 100), random.Next((int)playArea.ActualHeight - 100), new PropertyPath("(Canvas.Top)"));
            asteroids.Add(asteroid);
            playArea.Children.Add(asteroid);

            asteroid.MouseEnter += Asteroid_MouseEnter;
        }

        /// <summary>
        /// Ustawia losową pozycję i kąt rakiety na planszy.
        /// </summary>
        private void SetRocketPosition()
        {
            rocket.Visibility = Visibility.Visible;

            do
            {
                Canvas.SetLeft(rocket, random.Next((int)playArea.ActualWidth - 400) + 200);
                Canvas.SetTop(rocket, random.Next((int)playArea.ActualHeight - 300) + 200);
            } while (Math.Sqrt(Math.Pow(Canvas.GetLeft(falsePlanet) - Canvas.GetLeft(rocket), 2) + Math.Pow(Canvas.GetTop(falsePlanet) - Canvas.GetTop(rocket), 2)) < 100
                    && Math.Sqrt(Math.Pow(Canvas.GetLeft(rocket) - Canvas.GetLeft(correctPlanet), 2) + Math.Pow(Canvas.GetTop(rocket) - Canvas.GetTop(correctPlanet), 2)) < 100);


            
            rocket.LayoutTransform = new RotateTransform(random.Next(360));
        }

        /// <summary>
        /// Ustawia losową pozycję planet na planszy.
        /// </summary>
        private void SetPlanetPosition()
        {
            correctPlanet.Visibility = Visibility.Visible;
            falsePlanet.Visibility = Visibility.Visible;

            Canvas.SetLeft(correctPlanet, (random.Next((int)playArea.ActualWidth - 200 - (int)correctPlanet.ActualWidth)) + 100);
            Canvas.SetTop(correctPlanet, (random.Next((int)playArea.ActualHeight - 100 - (int)correctPlanet.ActualHeight)) + 50);

            do
            {
                Canvas.SetLeft(falsePlanet, (random.Next((int)playArea.ActualWidth - 200 - (int)correctPlanet.ActualWidth)) + 100);
                Canvas.SetTop(falsePlanet, (random.Next((int)playArea.ActualHeight - 100 - (int)correctPlanet.ActualHeight)) + 50);
            } while (Math.Sqrt(Math.Pow(Canvas.GetLeft(falsePlanet) - Canvas.GetLeft(correctPlanet), 2) + Math.Pow(Canvas.GetTop(falsePlanet) - Canvas.GetTop(correctPlanet), 2)) < 400);


        }

        /// <summary>
        /// Ustawia luki słów na planetach.
        /// </summary>
        private void SetPlanetContent()
        {
            if (!QuestionSelector.IsGapsListEmpty)
            {
                correctLabel.Content = QuestionSelector.gaps[0];

                switch (QuestionSelector.gaps[0])
                {
                    case "h": falseLabel.Content = "ch"; break;
                    case "ch": falseLabel.Content = "h"; break;
                    case "ż": falseLabel.Content = "rz"; break;
                    case "rz": falseLabel.Content = "ż"; break;
                    case "u": falseLabel.Content = "ó"; break;
                    case "ó": falseLabel.Content = "u"; break;
                }
            }
           
        }

        /// <summary>
        /// Ustawia słowo z pytaniem na planszy.
        /// </summary>
        private void SetQuestionLabel()
        {
            wordToDisplayLabel.Content = QuestionSelector.wordToDisplay;
        }

        /// <summary>
        /// Wywołuje następne pytanie.
        /// </summary>
        private void NextQuestion()
        {
            QuestionSelector.AskQuestion();
            SetPlanetPosition();
            SetPlanetContent();
            SetQuestionLabel();
        }

        /// <summary>
        /// Aktualizuje punktację wyświetlaną na planszy.
        /// </summary>
        private void UpdateScoreLabel()
        {
            scoreLabel.Content = "Wynik: " + StatisticsCollector.Score();
        }

        /// <summary>
        /// Wywołuję funkcję ReleaseRocket() oraz animuje wybuch rakiety.
        /// </summary>
        private void DestroyRocket()
        {
            ReleaseRocket();
            isRocketThrustersSound = false;
            explosion.Play();

            Canvas.SetLeft(rocketExplosion, Canvas.GetLeft(rocket));
            Canvas.SetTop(rocketExplosion, Canvas.GetTop(rocket));

            rocketExplosion.Visibility = Visibility.Visible;
            explosionTimer.Start();
        }

        /// <summary>
        /// Wstrzymuje animację asteroid.
        /// </summary>
        private void PauseAsteroids()
        {
            foreach (Storyboard item in asteroidAnimationList)
            {
                item.Pause();
            }
        }

        /// <summary>
        /// Wznawia animację asteroid.
        /// </summary>
        private void StartAsteroids()
        {
            foreach (Storyboard item in asteroidAnimationList)
            {
                item.Resume();
            }
        }

        /// <summary>
        /// Usuwa asteroidy z planszy.
        /// </summary>
        private void RemoveAsteroids()
        {
            foreach (Storyboard item in asteroidAnimationList)
            {
                item.Stop();
            }

            foreach (ContentControl item in asteroids)
            {
                playArea.Children.Remove(item);
            }

            asteroidAnimationList.Clear();
            asteroids.Clear();
        }

        /// <summary>
        /// Wyświetla kontrolki menu. (Chowa wszystkie kontrolki związane z rozgrywką)
        /// </summary>
        public void DisplayMenu()
        {
            scoreLabel.Visibility = Visibility.Collapsed;
            falsePlanet.Visibility = Visibility.Collapsed;
            correctPlanet.Visibility = Visibility.Collapsed;
            help_label.Visibility = Visibility.Collapsed;
            help_label1.Visibility = Visibility.Collapsed;

            logInButton.Visibility = Visibility.Visible;
            statisticsButton.Visibility = Visibility.Visible;
            howToPlayButton.Visibility = Visibility.Visible;

            if (StatisticsCollector.loggedUser == StatisticsCollector.guestUser)
            {
                logInButton.Visibility = Visibility.Visible;
                logOutButton.Visibility = Visibility.Collapsed;
                loggedUserLabel.Visibility = Visibility.Collapsed;
            }
            else
            {

                logInButton.Visibility = Visibility.Collapsed;
                logOutButton.Visibility = Visibility.Visible;
                loggedUserLabel.Visibility = Visibility.Visible;
            }

            if (gameStarted)
            {
                wordToDisplayLabel.Visibility = Visibility.Visible;
                endGameButton.Visibility = Visibility.Visible;
                startButton.Visibility = Visibility.Collapsed;
                logOutButton.Visibility = Visibility.Collapsed;
                logInButton.Visibility = Visibility.Collapsed;
                logo.Visibility = Visibility.Collapsed;
            }
            else
            {
                wordToDisplayLabel.Visibility = Visibility.Collapsed;
                logo.Visibility = Visibility.Visible;
                endGameButton.Visibility = Visibility.Collapsed;
                startButton.Visibility = Visibility.Visible;
                rocket.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Wyświetla kontrolki gry. (Chowa wszystkie kontrolki menu)
        /// </summary>
        private void DisplayGameControls()
        {
            statisticsButton.Visibility = Visibility.Collapsed;
            endGameButton.Visibility = Visibility.Collapsed;
            startButton.Visibility = Visibility.Collapsed;
            logInButton.Visibility = Visibility.Collapsed;
            logOutButton.Visibility = Visibility.Collapsed;
            logo.Visibility = Visibility.Collapsed;
            howToPlayButton.Visibility = Visibility.Collapsed;


            wordToDisplayLabel.Visibility = Visibility.Visible;
            scoreLabel.Visibility = Visibility.Visible;
            loggedUserLabel.Visibility = Visibility.Visible;
            falsePlanet.Visibility = Visibility.Visible;
            correctPlanet.Visibility = Visibility.Visible;
            rocket.Visibility = Visibility.Visible;
            help_label.Visibility = Visibility.Visible;
            help_label1.Visibility = Visibility.Visible;
        }

    }
}
