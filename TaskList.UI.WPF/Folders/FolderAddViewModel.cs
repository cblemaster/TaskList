using System.ComponentModel;
using TaskList.UI.Services.Models;

namespace TaskList.UI.WPF.Folders
{
    public class FolderAddViewModel : INotifyPropertyChanged
    {
        public FolderAddViewModel()
        {

        }

        private Folder _folder = new();

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
        public Folder Folder
        {
            get { return _folder; }
            set
            {
                if (value != _folder)
                {
                    _folder = value;
                    PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Folder)));
                };
            }
        }
    }
}
