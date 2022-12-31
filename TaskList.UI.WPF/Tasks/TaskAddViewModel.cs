using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.UI.Services;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Tasks
{
    public class TaskAddViewModel : INotifyPropertyChanged
    {
        public TaskAddViewModel()
        {
            this.Task = new();
        }

        //private readonly TaskService _ts = new();
        private Task _task = new();

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public Task Task
        {
            get { return _task; }
            set
            {
                if (value != _task)
                {
                    _task = value;
                    PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Task)));
                };
            }
        }

        //public static DateTime TodaysDate()
        //{
        //    return DateTime.Today;   
        //}
    }
}
