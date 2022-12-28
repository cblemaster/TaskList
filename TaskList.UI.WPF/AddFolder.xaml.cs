using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using TaskList.UI.Services;
using TaskList.UI.Services.Models;
using TaskList.UI.Services.Validation;

namespace TaskList.UI.WPF
{
    /// <summary>
    /// Interaction logic for AddFolder.xaml
    /// </summary>
    public partial class AddFolder : Window
    {
        public AddFolder()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewFolder nf = new() { FolderName = this.tbNewFolderName.Text };
            (bool isValid, List<string> validationErrors) validationResult = ModelValidation.ValidateFolder(nf);

            if (validationResult.isValid)
            {
                FolderService fs = new();
                fs.Create(nf);
                this.Close();
            }
            else
            {
                if (validationResult.validationErrors.Any())
                {
                    StringBuilder sb = new();
                    sb.AppendLine("The following errors occured:");
                    validationResult.validationErrors.ForEach(v => sb.AppendLine(v));

                    this.tbErrors.Text = sb.ToString();
                    this.tbErrors.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
