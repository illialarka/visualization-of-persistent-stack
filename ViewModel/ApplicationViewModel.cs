using PersistentStackVisualization.PaintModule;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using PersistentStackVisualization.ViewModel;
using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace PersistentStackVisualization
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private string _value;

        private string _version;

        private string _popVersion;

        private string _consoleText;

        /// <summary>
        /// Constructor for ViewModel
        /// </summary>
        /// <param name="targetStackPanel"> Target stack panel for drawing </param>
        public ApplicationViewModel()
        { }

        public string ConsoleText
        {
            get => _consoleText;
            set
            {
                _consoleText += value;
                OnPropertyChanged("ConsoleText");
            }
        }

        /// <summary>
        /// Binding value push node
        /// </summary>
        public string ValueNode
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("ValudeNode");
            }
        }

        /// <summary>
        /// Binding version push node
        /// </summary>
        public string VersionNode
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged("VersionNode");
            }
        }

        /// <summary>
        /// Binding pop version
        /// </summary>
        public string PopVersion
        {
            get => _popVersion;
            set
            {
                _popVersion = value;
                OnPropertyChanged("PopVersion");
            }
        }

        /// <summary>
        /// Run test
        /// </summary>
        private Command.Command _runTest;

        /// <summary>
        /// Run test
        /// </summary>
        public Command.Command RunTest
        {
            get
            {
                return _runTest ??
                    (
                    _runTest = new Command.Command(
                        obj =>
                        {
                            using (XmlReader readTest = XmlReader.Create("test.xml"))
                            {
                                readTest.MoveToContent();
                                while(readTest.Read())
                                {
                                    if (readTest.Name == "command")
                                    { 
                                        readTest.MoveToAttribute("type");
                                        string type = readTest.ReadContentAsString();
                                        if(type == "push")
                                        {
                                            readTest.MoveToAttribute("version");
                                            string version = readTest.ReadContentAsString();
                                            VersionNode = version;
                                            readTest.MoveToAttribute("value");
                                            string value = readTest.ReadContentAsString();
                                            ValueNode = value;
                                            AddVersion.Execute(obj);
                                        }
                                        if(type == "pop")
                                        {
                                            readTest.MoveToAttribute("version");
                                            string version = readTest.ReadContentAsString();
                                            PopVersion = version;
                                            PopCommand.Execute(obj);
                                        }
                                        
                                    }
                                }                            
                            }
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Command for add version into StackPanel for version
        /// </summary>
        private Command.Command _addVersion;

        /// <summary>
        /// Command add version
        /// </summary>
        public Command.Command AddVersion
        {
            get
            {
                return _addVersion ??
                    (_addVersion = new Command.Command(
                        version =>
                        {
                            try
                            {
                                WriteConsole(String.Format("Push ({0}, {1})", VersionNode, ValueNode));
                                StackPanel target = (StackPanel)version;
                                target.Children.Add(new VersionElement().WrapperPush(VersionNode, ValueNode));
                                TraceLog.Logger.LogInformation(String.Format("Push ({0}, {1})", VersionNode, ValueNode));
                            }
                            catch(Exception exception)
                            {
                                MessageBox.Show("Version dose not exist (" + exception.Message + ")", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                                if (VersionElement.NumberVersion != -1) VersionElement.NumberVersion = 0;
                                WriteConsole("Version dose not exist");
                                TraceLog.Logger.LogError("Version dose not exist");
                            }
                        }
                        ));
            }
        }


        /// <summary>
        /// Command pop from version
        /// </summary>
        private Command.Command _popCommand;

        /// <summary>
        /// Command for pop from version stack
        /// </summary>
        public Command.Command PopCommand
        {
            get
            {
                return _popCommand ??
                    (
                    _popCommand = new Command.Command(
                        obj =>
                        {
                            try
                            {
                                WriteConsole(String.Format("Pop ({0})", PopVersion));
                                StackPanel target = (StackPanel)obj;
                                target.Children.Add(new VersionElement().WrapperPop(PopVersion));
                                TraceLog.Logger.LogInformation(String.Format("Pop ({0})", PopVersion));
                            }
                            catch(Exception exception)
                            {
                                MessageBox.Show("Version dose not exist (" + exception.Message + ")", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                                WriteConsole("# ERROR Version dose not exist");
                                TraceLog.Logger.LogError("Version dose not exist");
                            }
                        },
                        check =>
                        {
                            return VersionElement.PersistentStack.Count > 1;
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Destroy stack command
        /// </summary>
        private Command.Command _destroyStack;

        /// <summary>
        /// Destroy stack command and clear paint area
        /// </summary>
        public Command.Command DestroyStack
        {
            get
            {
                return _destroyStack ??
                    (
                    _destroyStack = new Command.Command(
                        obj =>
                        {
                            new VersionElement().WrapperDestroyStack();
                            StackPanel target = (StackPanel)obj;
                            target.Children.Clear();
                            WriteConsole("Destroy stack and clear paint area");
                            TraceLog.Logger.LogInformation("Destroy stack and clear paint area");
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Command save log file (.txt)
        /// </summary>
        private Command.Command _saveLogFile;

        /// <summary>
        /// Command save log file
        /// </summary>
        public Command.Command SaveLog
        {
            get
            {
                return _saveLogFile ??
                    (
                    _saveLogFile = new Command.Command(
                        obj =>
                        {
                            try
                            {
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                if (saveFileDialog.ShowDialog() == true)
                                    File.WriteAllText(saveFileDialog.FileName, ConsoleText);
                            }
                            catch(Exception exception)
                            {
                                MessageBox.Show("Something is not good (" + exception.Message + ")", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                WriteConsole("# Warning Can not open dialog save file");
                                TraceLog.Logger.LogWarning("Can not open dialog save file");
                            }
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Command clean console
        /// </summary>
        private Command.Command _cleanLog;


        /// <summary>
        /// Command clean console
        /// </summary>
        public Command.Command CleanConsole
        {
            get
            {
                return _cleanLog ??
                    (
                    _cleanLog = new Command.Command(
                        obj =>
                        {
                            ConsoleText = "";
                            TraceLog.Logger.LogInformation("Console has cleaned");
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Window test
        /// </summary>
        private Command.Command _openTestWindow;

        /// <summary>
        /// Window test show
        /// </summary>
        public Command.Command OpenTestWindow
        {
            get
            {
                return _openTestWindow ??
                    (_openTestWindow = new Command.Command(
                        obj =>
                        {
                            new TestLoadWindow().Show();
                        },
                        (obj) =>
                        {
                            return !TestLoadViewModel.IsOpen;
                        }
                        )
                    );
            }
        }

        /// <summary>
        /// Write console
        /// </summary>
        /// <param name="Message"> Message </param>
        public void WriteConsole(string Message)
        {
            ConsoleText = "\n" + Message + " " + DateTime.Now;
        }

        /// <summary>
        /// Handler change property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Changer property colled event
        /// </summary>
        /// <param name="property"> Name property </param>
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
