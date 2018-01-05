using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PersistentStackVisualization.PaintModule
{
    /// <summary>
    /// Number line for content test
    /// </summary>
    public class NumberLineTestText : StackGraphics
    {
        private static int _numberLine = 0;

        /// <summary>
        /// Constructo for instance
        /// </summary>
        public NumberLineTestText()
        {
            _numberLine++;
        }


        /// <summary>
        /// Init element
        /// </summary>
        /// <returns> Initialized element </returns>
        public override UIElement Init()
        {
            TextBlock textLineNumber = new TextBlock()
            {
                Text = _numberLine.ToString(),
                Background = Brushes.LightGray,
                MinWidth = 25,
                Width = Double.NaN,
                Foreground = Brushes.Blue,
                TextAlignment = TextAlignment.Center
            };
            return textLineNumber;
        }


        /// <summary>
        /// Reset value
        /// </summary>
        public static void Reset() => _numberLine = 0;
    }
}
