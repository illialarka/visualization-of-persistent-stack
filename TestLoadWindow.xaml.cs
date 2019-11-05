using DataAccessLayer;
using System.Data.Entity;
using System.Windows;

namespace PersistentStackVisualization
{
    /// <summary>
    /// Interaction logic for TestLoadWindow.xaml
    /// </summary>
    public partial class TestLoadWindow : Window
    {
        private StackContext context;

        public TestLoadWindow()
        {
            InitializeComponent();
            context = new StackContext();

            context.Records.Load();
            logRecords.ItemsSource = context.Records.Local.ToBindingList();
            Closing += MainWindow_Closing; ;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (logRecords.SelectedItems.Count > 0)
            {
                for (int i = 0; i < logRecords.SelectedItems.Count; i++)
                {
                    var record = logRecords.SelectedItems[i] as LogRecord;
                    if (record != null)
                    {
                        context.Records.Remove(record);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
