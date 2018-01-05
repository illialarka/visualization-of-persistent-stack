using System.Collections.Generic;
using System;

namespace PersistentStackVisualization.ImplementPersistentStack
{
    /// <summary>
    /// Persistent class stack
    /// </summary>
    public class PersistentStack : IPersistentStack<int>
    {
        private static List<Node> _version = new List<Node>();


        public int Count => _version.Count;
        /// <summary>
        /// Constructor for a persistent stack instance
        /// </summary>
        public  PersistentStack()
        {
            _version.Add(new Node(int.MinValue, 0));
        }

        /// <summary>
        /// Extract value from stack
        /// </summary>
        /// <param name="version"> Stack version </param>
        /// <returns> Value stack version </returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public int Pop(int version)
        {
            Node curr = _version[version], prev = _version[curr.Previously];

            _version.Add(new Node(prev.Value, prev.Previously));

            return curr.Value;
        }

        /// <summary>
        /// Add value to persistent stack version
        /// </summary>
        /// <param name="version"> Stack version </param>
        /// <param name="value"> Insert value </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Push(int version, int value)
        {
            if (version > _version.Count - 1 || version < 0)
                throw new ArgumentOutOfRangeException();
            _version.Add(new Node(value, version));
        }

        /// <summary>
        /// Get the IEnumerable<int> of the specified version of the stack
        /// </summary>
        /// <param name="version"> Stack version </param>
        /// <returns> IEnumerable<int> </returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public IEnumerable<Node> GetVersion(int version)
        {
            Node curr = _version[version];
            while (curr.Value != int.MinValue)
            {
                yield return curr;
                curr = _version[curr.Previously];
            }
        }

        /// <summary>
        /// Destroy with GC
        /// </summary>
        public void Clear()
        {
            _version.Clear();
            _version = null;
            _version = new List<Node>();
            _version.Add(new Node(int.MinValue, 0));
        }
    }
}
