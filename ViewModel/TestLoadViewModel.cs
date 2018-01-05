using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using PersistentStackVisualization.PaintModule;

namespace PersistentStackVisualization.ViewModel
{
    public class TestLoadViewModel : INotifyPropertyChanged
    {
        private string _testText;

        public static bool IsOpen;

        private TextBox _targetTextBlock;

        /// <summary>
        /// ViewModel for test window
        /// </summary>
        /// <param name="targetTextBlock"></param>
        public TestLoadViewModel(TextBox targetTextBlock)
        {
            _targetTextBlock = targetTextBlock;
            IsOpen = true;
        }

        /// <summary>
        /// Property for test content
        /// </summary>
        public string TestContent
        {
            get => _testText;
            set
            {
                if (_testText == value)
                    return;
                _testText = value;
                OnPropertyChanged("TestContent");
            }
        }

        /// <summary>
        /// Open test from file
        /// </summary>
        private Command.Command _openFile;

        /// <summary>
        /// Open test from file
        /// </summary>
        public Command.Command OpenFile
        {
            get
            {
                return _openFile ??
                    (
                    _openFile = new Command.Command(
                        obj =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                                TestContent = File.ReadAllText(openFileDialog.FileName);
                            ParseTest.Pearser.Run(TestContent);

                            StackPanel target = (StackPanel)obj;

                            for (int index = 0; index <= _targetTextBlock.LineCount; ++index)
                            {
                                NumberLineTestText numberLine = new NumberLineTestText();
                                target.Children.Add(numberLine.Init());
                            }
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Command save test
        /// </summary>
        private Command.Command _saveTest;


        /// <summary>
        /// Command save test
        /// </summary>
        public Command.Command SaveTest
        {
            get
            {
                return _saveTest ??
                    (
                    _saveTest = new Command.Command(
                        obj =>
                        {
                            ParseTest.Pearser.Run(_targetTextBlock.Text);
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Clear content test
        /// </summary>
        private Command.Command _clearContentTest;

        /// <summary>
        /// Clear content test
        /// </summary>
        public Command.Command ClearContentTest
        {
            get
            {
                return _clearContentTest ??
                    (
                    _clearContentTest = new Command.Command(
                        obj =>
                        {
                            TestContent = "";
                            StackPanel target = (StackPanel)obj;
                            NumberLineTestText.Reset();
                            target.Children.Clear();
                        }
                        )
                    );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
