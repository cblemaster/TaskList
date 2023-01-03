using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;

namespace TaskList.UI.WPF.Folders
{
    public class FolderListViewModel : INotifyPropertyChanged
    {
        public FolderListViewModel()
        {
            this.Folders = new ObservableCollection<Folder>(_fs.GetAll());
            foreach (Folder f in this.Folders)
            {
                switch (f.FolderName)
                {
                    case "Planned":
                        f.Tasks = new ObservableCollection<Task>(_ts.GetPlanned());
                        break;
                    case "Completed":
                        f.Tasks = new ObservableCollection<Task>(_ts.GetCompleted());
                        break;
                    case "Recurring":
                        f.Tasks = new ObservableCollection<Task>(_ts.GetRecurring());
                        break;
                    case "Important":
                        f.Tasks = new ObservableCollection<Task>(_ts.GetImportant());
                        break;
                }
            }
        }

        private readonly FolderService _fs = new();
        private readonly TaskService _ts = new();
        private ObservableCollection<Folder> _folders = new();

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
    }
}
