using System.Collections.ObjectModel;
using System.Windows.Controls;
using TaskList.UI.Services.Models;
using TaskList.UI.WPF.Tasks;

namespace TaskList.UI.WPF.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();

            this.folderListView.folderList.SelectionChanged += this.FolderList_SelectionChanged;
            this.taskListView.taskList.SelectionChanged += this.TaskList_SelectionChanged;
        }

        private void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((TaskDetailViewModel)this.taskDetailView.DataContext).Task = new();

            if (this.taskListView.DataContext is TaskListViewModel && this.folderListView.folderList.SelectedItem is Folder)
            {
                ((TaskListViewModel)this.taskListView.DataContext).Tasks = new ObservableCollection<Task>(((Folder)(this.folderListView.folderList.SelectedItem)).Tasks);
            }
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.taskDetailView.DataContext is TaskDetailViewModel && this.taskListView.taskList.SelectedItem is Task)
            {
                ((TaskDetailViewModel)this.taskDetailView.DataContext).Task = (Task)this.taskListView.taskList.SelectedItem;
            }
        }
    }
}
