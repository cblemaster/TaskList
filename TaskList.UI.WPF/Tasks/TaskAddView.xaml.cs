using System.Windows;
using System.Windows.Controls;

namespace TaskList.UI.WPF.Tasks
{
    /// <summary>
    /// Interaction logic for TaskAddView.xaml
    /// </summary>
    public partial class TaskAddView : UserControl
    {
        public TaskAddView()
        {
            InitializeComponent();
            this.dpDueDate.BlackoutDates.AddDatesInPast();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
