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

namespace Pomodoro
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class TagChoice : Window
    {     

        public TagChoice()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow accueilWindow = new MainWindow();
            accueilWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var taskName = taskNameInput.Text;
            var nbPomodoro = int.Parse(nbPomodoroInput.Text);
            Task task = new Task(taskName, nbPomodoro);            
            if (MainWindow.tasksList.ContainsKey(taskName))
            {
                MainWindow.tasksList[taskName].nbPomodoro = task.nbPomodoro;
            }
            else
            {
                MainWindow.tasksList.Add(taskName, task);
                taskList.Items.Add(task.name);
            }
            taskNameInput.Text = "";
            nbPomodoroInput.Text = "";
            labelTache.Content = "Nouvelle tâche";
            addTaskButton.Content = "Ajouter une tâche";
            taskNameInput.IsEnabled = true;
        }
        private void taskList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelTache.Content = "Modification d'une tâche";
            if(taskList.SelectedItems.Count == 1)
            {
                taskNameInput.Text = MainWindow.tasksList[taskList.SelectedItem.ToString()].name;
                taskNameInput.IsEnabled = false;
                nbPomodoroInput.Text = MainWindow.tasksList[taskList.SelectedItem.ToString()].nbPomodoro.ToString();                
            }
            addTaskButton.Content = "Modifier";
        }

        private void beginButton_Click(object sender, RoutedEventArgs e)
        {
            PomodoroFinal pomodoroFinal = new PomodoroFinal();
            MainWindow mw = new MainWindow();
            pomodoroFinal.Show();
            this.Close();
        }

        private void nbPomodoroInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(nbPomodoroInput.Text, "[^0-9]"))
            {
                MessageBox.Show("Veuillez entrer seulement des nombres");
                nbPomodoroInput.Text = nbPomodoroInput.Text.Remove(nbPomodoroInput.Text.Length - 1);
            }
        }
    }
}
