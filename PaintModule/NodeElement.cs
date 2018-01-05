using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PersistentStackVisualization.ImplementPersistentStack;

namespace PersistentStackVisualization.PaintModule
{
    /// <summary>
    /// Node graphics
    /// </summary>
    public class NodeElement : StackGraphics
    {
        private Node _node;

        /// <summary>
        /// Constructor for a persistent stack instance
        /// </summary>
        /// <param name="node"> Safate node </param>
        public NodeElement(Node node)
        {
            _node = node;
        }

        /// <summary>
        /// Display node element
        /// </summary>
        public override UIElement Init()
        {
            Canvas groupBox = new Canvas()
            {
                Width = 70,
                Height = 70,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            };
            TextBlock valueNode = new TextBlock()
            {
                Text = _node.Value.ToString(),
                FontSize = 20,
                Margin = new System.Windows.Thickness(30, 20, 0, 0)
            };
            Ellipse circleNode = new Ellipse()
            {
                Stroke = Brushes.Black,
                Width = 50,
                Height = 50,
                Fill = Brushes.Green,
                Margin = new System.Windows.Thickness(10)
            };
            groupBox.Children.Add(circleNode);
            groupBox.Children.Add(valueNode);
            return groupBox;
        }

    }
}
