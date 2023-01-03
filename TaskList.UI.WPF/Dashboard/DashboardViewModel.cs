using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Dashboard
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public DashboardViewModel()
        {

        }

        private readonly FolderService _fs = new();
        private readonly TaskService _ts = new();

        private ObservableCollection<Folder> _folders = new();
        private ObservableCollection<Task> _tasks = new();

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public ObservableCollection<Folder> Folders
        {
            get { return _folders; }
            set
            {
                if (value != _folders)
                {
                    _folders = value;
                    PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Folders)));
                };
            }
        }

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
