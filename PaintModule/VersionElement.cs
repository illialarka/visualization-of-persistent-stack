using System;
using System.Windows;
using System.Windows.Controls;
using PersistentStackVisualization.ImplementPersistentStack;

namespace PersistentStackVisualization.PaintModule
{
    /// <summary>
    /// Version graphics
    /// </summary>
    public class VersionElement : StackGraphics
    {
        private static int _numberVersion = -1;

        /// <summary>
        /// Persistent stack 
        /// </summary>
        public static PersistentStack PersistentStack = new PersistentStack();

        /// <summary>
        /// Property for number version
        ///// </summary>
        public static int NumberVersion
        {
            get => _numberVersion;
            set
            {
                if (value < -1) throw new Exception("Number version has to be more then -1");
                if (value > PersistentStack.Count) throw new Exception("Number version has to be less then current");
                _numberVersion = value;
            }
        }

        /// <summary>
        /// Panel for name
        /// </summary>
        public string Name;

        //private StackPanel newVersionPanel;
        /// <summary>
        /// Constructor for new version
        /// </summary>
        public VersionElement()
        {
            ++_numberVersion;
        }

        /// <summary>
        /// Display element
        /// </summary>
        public override UIElement Init()
        {
            StackPanel stackPanelNewVersion = new StackPanel()
            {
                Name = "Version" + _numberVersion.ToString(),
                Orientation = Orientation.Horizontal,
                Margin = new System.Windows.Thickness(10, 0, 0, 0)
            };

            TextBlock textVersion = new TextBlock()
            {
                Text = (_numberVersion + 1).ToString(),
                FontSize = 20,
                Margin = new System.Windows.Thickness(30)
            };

            stackPanelNewVersion.Children.Add(textVersion);

            foreach (Node n in PersistentStack.GetVersion(_numberVersion + 1))
            {
                stackPanelNewVersion.Children.Add(new NodeElement(n).Init());
            }

            return stackPanelNewVersion;
        }


        /// <summary>
        /// Wrap under push
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Value"></param>
        public UIElement WrapperPush(string Version, string Value)
        {
            PersistentStack.Push(int.Parse(Version), int.Parse(Value));
            return this.Init();
        }

        /// <summary>
        /// Wrapper under pop
        /// </summary>
        /// <param name="Version"></param>
        public UIElement WrapperPop(string Version)
        {
            PersistentStack.Pop(int.Parse(Version));
            return this.Init();
        }

        /// <summary>
        /// Wraper under destroy stack
        /// </summary>
        public void WrapperDestroyStack()
        {
            PersistentStack.Clear();
            //NumberVersion = -1;
        }
    }
}
