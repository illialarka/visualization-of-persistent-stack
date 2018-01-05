using System.Windows;
using PersistentStackVisualization.TraceLog;


namespace PersistentStackVisualization
{
    /// <summary>
    /// Class for main window application
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Logger.LogInformation("\n\nStart app\n\n");
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}