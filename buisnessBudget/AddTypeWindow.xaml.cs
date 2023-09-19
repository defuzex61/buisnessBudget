using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace BudgetTracker.View
{
    public partial class NewTypeWindow : Window
    {

        public NewTypeWindow()
        {
            InitializeComponent();
        }

        private void btnSaveType_Click(object sender, RoutedEventArgs e)
        {
            string text = tbNewType.Text;
            MainWindow secondWindow = new MainWindow(text);
            secondWindow.Show();
            this.Close();
        }

        private void btnCancelType_Click(object sender, RoutedEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
            Close();
        }
    }
}
