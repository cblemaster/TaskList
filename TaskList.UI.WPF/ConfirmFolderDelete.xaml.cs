using System.Windows;
using TaskList.UI.Services;

namespace TaskList.UI.WPF
{
    /// <summary>
    /// Interaction logic for ConfirmFolderDelete.xaml
    /// </summary>
    public partial class ConfirmFolderDelete : Window
    {
        public ConfirmFolderDelete()
        {
            InitializeComponent();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            FolderService fs = new();
            fs.Delete((int)this.Tag);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
