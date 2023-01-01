using System.Windows;
using System.Windows.Controls;

namespace TaskList.UI.WPF.Tasks
{
    /// <summary>
    /// Interaction logic for TaskEditView.xaml
    /// </summary>
    public partial class TaskEditView : UserControl
    {
        public TaskEditView()
        {
            InitializeComponent();
            //this.dpDueDate.BlackoutDates.AddDatesInPast();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
