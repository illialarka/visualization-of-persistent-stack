namespace PersistentStackVisualization.ImplementPersistentStack
{
    /// <summary>
    /// Class of node for persistant stack
    /// </summary>
    public class Node
    {
        private int _value;

        private int _prev;

        /// <summary>
        /// Constructor for node
        /// </summary>
        /// <param name="numberVersion"> Number version of stack </param>
        /// <param name="value"> Value of node </param>
        public Node(int value, int prev)
        {
            Value = value;
            Previously = prev;
        }

        /// <summary>
        /// Node's value
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Previously node into stack
        /// </summary>
        public int Previously
        {
            get => _prev;
            set
            {
                _prev = value;
            }
        }
    }
}
