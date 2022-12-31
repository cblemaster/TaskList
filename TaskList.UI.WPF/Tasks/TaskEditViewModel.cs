using System.ComponentModel;
using TaskList.UI.Services;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Tasks
{
    internal class TaskEditViewModel : INotifyPropertyChanged
    {
        public TaskEditViewModel()
        {
            GetTaskToEdit(12);
        }

        private readonly TaskService _ts = new();
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

        private void GetTaskToEdit(int id)
        {
            this.Task = _ts.GetById(id);
        }

        private void Save()
        {
            _ts.Update(Task);
        }
    }
}
