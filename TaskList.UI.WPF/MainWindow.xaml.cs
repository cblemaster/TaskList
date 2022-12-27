using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;

// TODO:
// 1. Hide rename/delete buttons except for selected row in folder list
// 2. Move model validation to UI; tests will also need to be fixed up
//      2.a. UI will call validation methods, display any errors
// 3. Add folder functionality
// 4. Rename folder functionality
// 5. Delete folder functionality
// 6. Add task functionality
//      6.a. 'Tasks' is the default folder for new tasks created from:
//              'Planned', 'Complete', 'Important', 'Recurring'
//              OR if folder name not specified; add this to validation 
// 7. Edit task functionality
// 8. Delete task functionality
// REMEMBER: Cascading updates and deletes are ON

namespace TaskList.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.TaskDetailsGrid.Visibility = Visibility.Collapsed;
            this.btnEditTask.Visibility = Visibility.Collapsed;
            this.btnDeleteTask.Visibility = Visibility.Collapsed;

            FolderService fs = new();
            List<Folder> folders = fs.GetAll();
            this.FolderList.ItemsSource = folders;
        }

        private void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TaskDetailsGrid.Visibility = Visibility.Collapsed;
            this.btnEditTask.Visibility = Visibility.Collapsed;
            this.btnDeleteTask.Visibility = Visibility.Collapsed;

            this.TaskList.SelectedItem = null;

            TaskService ts = new();
            Folder selectedFolder = (Folder)e.AddedItems[0]!;

            List<Task> tasks = selectedFolder.FolderName switch
            {
                "Important" => ts.GetImportant().ToList(),
                "Completed" => ts.GetCompleted().ToList(),
                "Recurring" => ts.GetRecurring().ToList(),
                "Planned" => ts.GetPlanned().ToList(),
                _ => selectedFolder.Tasks.ToList(),
            };
            
            this.TaskList.ItemsSource = tasks;            
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Task selectedTask = (Task)e.AddedItems[0]!;

                this.TaskDetailsGrid.Visibility = Visibility.Visible;
                this.btnEditTask.Visibility = Visibility.Visible;
                this.btnDeleteTask.Visibility = Visibility.Visible;

                tbTaskId.Text = selectedTask.Id.ToString();
                tbTaskName.Text = selectedTask.TaskName;
                tbDueDate.Text = selectedTask.DueDate == null ? string.Empty : ((DateTime)selectedTask.DueDate).ToString("d");
                tbRecurrence.Text = selectedTask.Recurrence.ToString();
                cbImportant.IsChecked = selectedTask.IsImportant;
                cbComplete.IsChecked = selectedTask.IsComplete;
                tbNote.Text = selectedTask.Note ?? string.Empty;
            }          
        }

        private void btnRenameFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
