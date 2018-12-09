// Author: Joon Song
// File Name: IQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Interface to contain definitions for a generic Queue

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public interface IQueue<T>
    {
        /// <summary>
        /// The number of items in the queue
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Whether the queue is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Subprogram to add an item to the end of a queue
        /// </summary>
        /// <param name="item">The item to be added</param>
        void Enqueue(T item);

        /// <summary>
        /// Subprogram to remove and return the item in front of a queue
        /// </summary>
        /// <returns>The item in front of the queue</returns>
        T Dequeue();

        /// <summary>
        /// Subprogram to peek the item in front of a queue
        /// </summary>
        /// <returns>The item in front of the queue</returns>
        T Peek();
    }
}
