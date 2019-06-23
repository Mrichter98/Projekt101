using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Projekt101

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>


    public partial class MainWindow : Window
    {
        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();

        public MainWindow()

        {

            InitializeComponent();

        }
        // metoda odpowiedzalna za przekazanie pliku video do programu
        private void Item0_Selected(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fil = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "*.*",
                Filter = "Media Files (*.*)|*.*"
            };
            fil.ShowDialog();
            try { VideoPl.Source = new Uri(fil.FileName); MessageBox.Show("Press 'Start' to watch MP4"); }
            catch { new NullReferenceException("Error"); }


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(Timer_tick);
            timer.Start();

        }
        /// <summary>
        /// Metoda odpowiedzialna za odwzorowaniu progresu video na odpowiednim sliderze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_tick(object sender, EventArgs e)
        {
            TimeSlider.Value = VideoPl.Position.TotalSeconds;
        }
        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            VideoPl.Position = ts;
        }
        

        private void VideoPl_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(VideoPl.NaturalDuration.TimeSpan.TotalMilliseconds);
            TimeSlider.Maximum = ts.TotalSeconds;
        }

        //START
        private void Item1_Selected(object sender, RoutedEventArgs e)
        {
            VideoPl.Play();
        }

        //STOP
        private void Item2_Selected(object sender, RoutedEventArgs e)
        {
            VideoPl.Pause();
        }

        //FULL SCREEN
        private void Item3_Selected(object sender, RoutedEventArgs e)
        {
            VideoPl.IsMuted = true;


        }
        private void Item3_Unselected(object sender, RoutedEventArgs e)
        {
            VideoPl.IsMuted = false;
        }

        //AUTOR
        private void Item4_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Marek Richter - w58930");
        }

        //IKONKA ZAMKNIĘCIA
        private void OffApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //IKONKA MINIMALIZACJI
        private void PlayDown(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //USTAWIENIA SZYBKOSCI
        private void VidSpeed075(object sender, RoutedEventArgs e)
        {
            VideoPl.SpeedRatio -= 0.25;
        }

        private void VidSpeed1(object sender, RoutedEventArgs e)
        {
            VideoPl.SpeedRatio = 1;
        }

        private void VidSpeed125(object sender, RoutedEventArgs e)
        {
            VideoPl.SpeedRatio += 0.25;
        }
        //Full Screen Media Element click
        private void lewyprzyciskgora(object sender, MouseButtonEventArgs e)
        {
            if (!DoubleClickTimer.IsEnabled)
            {
                DoubleClickTimer.Start();
            }
            else
            {
                if (!fullscreen)
                {
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Normal;
                }

                fullscreen = !fullscreen;
            }
        }


    }
}

