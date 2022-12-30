using System.Collections;
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
        }

        private readonly FolderService _fs = new();
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
