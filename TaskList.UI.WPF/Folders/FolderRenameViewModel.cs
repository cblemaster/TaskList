using System.ComponentModel;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;

namespace TaskList.UI.WPF.Folders
{
    public class FolderRenameViewModel : INotifyPropertyChanged
    {
        public FolderRenameViewModel()
        {
            this.GetFolderToEdit(5);
        }

        private readonly FolderService _fs = new();
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

        private void GetFolderToEdit(int id)
        {
            this.Folder = _fs.GetById(id);
        }

        private void Save()
        {
            _fs.Update(Folder);
        }
    }
}
