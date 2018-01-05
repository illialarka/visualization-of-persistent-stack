using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistentStackVisualization.ImplementPersistentStack
{
    /// <summary>
    /// Interface for persistent stack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IPersistentStack<T> where T : struct
    {
        /// <summary>
        /// Method for push new value into stack version
        /// </summary>
        /// <param name="version"> Number version </param>
        /// <param name="value"> Value </param>
        void Push(int version, T value);

        /// <summary>
        /// Method for return value from stack version
        /// </summary>
        /// <param name="version"> Version </param>
        /// <returns> Value from stack </returns>
        T Pop(int version);
    }
}
