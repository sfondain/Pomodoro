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
        int nbPauses = 0;
        int nbPomodorosDone;

        public PomodoroFinal()
        {
            InitializeComponent();
            Timer();
        }

        private void Timer()
        {
            List<string> listTaskName = ListTask(MainWindow.tasksList);
            if (nbPomodorosDone < countNbPomodoro(MainWindow.tasksList))
            {
                if (!pause)
                {
                    _time = TimeSpan.FromSeconds(5);
                    labelTitre.Content = listTaskName[nbPomodorosDone];
                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    tbTime.Content = _time.ToString("c").Substring(3, 5);
                    if (_time.Seconds == 1)
                    {
                        System.Media.SoundPlayer player = new SoundPlayer(Properties.Resources.pom);
                        player.Play();
                    }
                    if (_time == TimeSpan.Zero)
                    {
                        _timer.Stop();
                        nbPomodorosDone++;
                        pause = true;
                        Timer();
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);
                    _timer.Start();
                }
                else
                {
                    labelTitre.Content = "Pause";
                    if (nbPauses == 3)
                    {
                        _time = TimeSpan.FromSeconds(900);
                        nbPauses = 0;
                    }
                    else
                    {
                        _time = TimeSpan.FromSeconds(300);
                        nbPauses++;
                    }

                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                    {
                        tbTime.Content = _time.ToString("c").Substring(3, 5);
                        if (_time.Seconds == 1)
                        {
                            SoundPlayer player = new SoundPlayer(Properties.Resources.pomPause);
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
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
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

        private int countNbPomodoro(Dictionary<string, Task> tasks)
        {
            int nbPomodoros = 0;
            foreach (var item in tasks)
            {
                nbPomodoros = nbPomodoros + item.Value.nbPomodoro;
            }
            return nbPomodoros;
        }

        private List<string> ListTask(Dictionary<string, Task> tasks)
        {
            List<string> listNameTasks = new List<string>();
            foreach(var item in tasks)
            {
                for (int i =0; i < item.Value.nbPomodoro; i++)
                {
                    listNameTasks.Add(item.Value.name);
                }
            }
            return listNameTasks;
        }
    }
}
