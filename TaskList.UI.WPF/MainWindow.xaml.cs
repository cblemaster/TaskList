﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;

// TODO:
// 1. Add folder - TESTING, including validation
// 2. Delete folder - TESTING
// 3. Rename folder - code, then TESTING, including validation
// 4. Add task - code, then TESTING, including validation
//      4.a. 'Tasks' is the default folder for new tasks created from:
//              'Planned', 'Completed', 'Important', 'Recurring'
//              OR if folder name not specified; add this to validation
// 5. Delete task - code, then TESTING
// 6. Edit task - code, then TESTING, including validation
// 7. (UI will call validation methods, display any errors)
// 8. Hide rename/delete buttons in folder list except for selected row in folder list
// 9. Hide rename/delete buttons in folder list ALWAYS for folders: Important, Completed, Recurring, Planned, Tasks
// 10. REMEMBER: Cascading updates and deletes are ON

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

            //this.TaskDetailsGrid.Visibility = Visibility.Collapsed;
            //this.btnEditTask.Visibility = Visibility.Collapsed;
            //this.btnDeleteTask.Visibility = Visibility.Collapsed;

            //FolderService fs = new();
            //List<Folder> folders = fs.GetAll();
            //this.FolderList.ItemsSource = folders;
        }

        private void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (var item in FolderList.Items)
            //{
            //    if (item is Folder && e.AddedItems[0] is Folder)
            //    {
            //        if (((Folder)item).Id == ((Folder)e.AddedItems[0]!).Id)
            //        {
            //            var c = (ListView)sender;
            //            var d = (GridView)c.View;
            //            foreach (GridViewColumn item2 in d.Columns)
            //            {
            //                var ee = (GridViewColumn)item2;
            //                if (!(ee.CellTemplate == null) && ee.CellTemplate is DataTemplate)
            //                {
            //                    var g = (DataTemplate)ee.CellTemplate;
            //                    g.FindName("btnRename", c);
                                
                                
            //                    //var h = g.LoadContent();
            //                    //((Button)h).Visibility = Visibility.Visible;
            //                }
            //            }
            //        }
            //    }
            //}
            
            //this.TaskDetailsGrid.Visibility = Visibility.Collapsed;
            //this.btnEditTask.Visibility = Visibility.Collapsed;
            //this.btnDeleteTask.Visibility = Visibility.Collapsed;

            //this.TaskList.SelectedItem = null;

            if (e.AddedItems.Count > 0)
            {
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

                //this.TaskList.ItemsSource = tasks;
            }            
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Task selectedTask = (Task)e.AddedItems[0]!;

                //this.TaskDetailsGrid.Visibility = Visibility.Visible;
                //this.btnEditTask.Visibility = Visibility.Visible;
                //this.btnDeleteTask.Visibility = Visibility.Visible;

                //tbTaskId.Text = selectedTask.Id.ToString();
                //tbTaskName.Text = selectedTask.TaskName;
                //tbDueDate.Text = selectedTask.DueDate == null ? string.Empty : ((DateTime)selectedTask.DueDate).ToString("d");
                //tbRecurrence.Text = selectedTask.Recurrence.ToString();
                //cbImportant.IsChecked = selectedTask.IsImportant;
                //cbComplete.IsChecked = selectedTask.IsComplete;
                //tbNote.Text = selectedTask.Note ?? string.Empty;
            }
        }

        private void btnRenameFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteFolder_Click(object sender, RoutedEventArgs e)
        {
            ConfirmFolderDelete cfd = new()
            {
                Tag = ((Folder)(((Button)e.OriginalSource).DataContext)).Id
            };
            cfd.ShowDialog();

            //FolderService fs = new();
            //List<Folder> folders = fs.GetAll();
            //this.FolderList.ItemsSource = folders;
        }

        private void btnAddFolder_Click(object sender, RoutedEventArgs e)
        {
            //AddFolder af = new();
            //af.ShowDialog();

            //FolderService fs = new();
            //List<Folder> folders = fs.GetAll();
            //this.FolderList.ItemsSource = folders;
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
