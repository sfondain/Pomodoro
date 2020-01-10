using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro
{
    public class Task
    {

        public Task(String name, int nbPomodoro)
        {
            this.name = name;
            this.nbPomodoro = nbPomodoro;
        }

        public String name { get; set; }
        public int nbPomodoro { get; set; }
    }
}
