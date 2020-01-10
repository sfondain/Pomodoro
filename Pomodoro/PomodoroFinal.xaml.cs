using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Logique d'interaction pour PomodoroFinal.xaml
    /// </summary>
    public partial class PomodoroFinal : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        bool pause;

        public PomodoroFinal()
        {
            InitializeComponent();
            Timer();
        }

        private void Timer ()
        {
            if (!pause)
            {
                

                _time = TimeSpan.FromSeconds(1500);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    tbTime.Content = _time.ToString("c").Substring(3, 5);
                    if(_time.Seconds == 3)
                    {
                        System.Media.SoundPlayer player = new SoundPlayer(@"D:\Epsi\pom.wav");
                        player.Play();
                    }
                    if (_time == TimeSpan.Zero)
                    {
                        _timer.Stop();
                        pause = true;
                        Timer();
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else
            {
                _time = TimeSpan.FromSeconds(300);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    tbTime.Content = _time.ToString("c").Substring(3, 5);
                    if (_time.Seconds == 3)
                    {
                        SoundPlayer player = new SoundPlayer(@"D:\Epsi\pomPause.wav");
                        player.Play();
                    }
                    if (_time == TimeSpan.Zero)
                    {
                        _timer.Stop();
                        pause = false;
                        Timer();
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void buttonLect_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }
    }
}
