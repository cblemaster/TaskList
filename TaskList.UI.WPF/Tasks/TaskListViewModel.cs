using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskList.UI.Services;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Tasks
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        public TaskListViewModel()
        {
            this.Tasks = new ObservableCollection<Task>(_ts.GetImportant());
        }

        private readonly TaskService _ts = new();
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
