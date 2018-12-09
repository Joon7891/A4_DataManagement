// Author: Joon Song
// File Name: ListQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 12/09/2018
// Modified Date: 12/09/2018
// Description: Class to hold ListQueue, a List implementation of IQueue

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public class ListQueue<T> : IQueue<T>
    {
        /// <summary>
        /// The size of the ListQueue
        /// </summary>
        public int Size => items.Count;

        /// <summary>
        /// Whether the ListQueue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        // List to hold the items in the queue
        protected List<T> items = new List<T>();

        /// <summary>
        /// Subprogram to add an item to the end of the ListQueue
        /// </summary>
        /// <param name="item">The item to be added</param>
        public virtual void Enqueue(T item)
        {
            // Adding item to the end of the ListQueue
            items.Add(item);
        }

        /// <summary>
        /// Subprogram to remove and return the item in front of the ListQueue
        /// </summary>
        /// <returns>The item in front of the ListQueue</returns>
        public virtual T Dequeue()
        {
            // Caching front item - initially set to type default
            T frontItem = default(T);

            // If Queue is not empty, set front item as actual front item and remove it
            if (!IsEmpty)
            {
                frontItem = items[0];
                items.RemoveAt(0);
            }

            // Returning front item
            return frontItem;
        }

        /// <summary>
        /// Subprogram to peek the item in front of the ListQueue
        /// </summary>
        /// <returns>The item in front of the ListQueue</returns>
        public virtual T Peek()
        {
            // Returing item in front of the ListQueue if it exists, otherwise returning the default
            return IsEmpty ? default(T) : items[0];
        }
    }
}
