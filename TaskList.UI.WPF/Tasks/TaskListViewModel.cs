using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Tasks
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        public TaskListViewModel()
        {

        }

        public TaskListViewModel(List<Task> tasks)
        {
            this.Tasks = new ObservableCollection<Task>(tasks);
        }

        private ObservableCollection<Task> _tasks = new();

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set
            {
                if (value != _tasks)
                {
                    _tasks = value;
                    PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Tasks)));
                };
            }
        }
    }
}
