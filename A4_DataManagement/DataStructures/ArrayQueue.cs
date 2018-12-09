// Author: Joon Song
// File Nmae: ArrayQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 12/09/2018
// Modified Date: 12/09/2018
// Description: Class to hold ArrayQueue, an Array implementation of IQueue

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public class ArrayQueue<T> : IQueue<T>
    {
        /// <summary>
        /// The max size of the ArrayQueue
        /// </summary>
        public int MaxSize { get; }

        /// <summary>
        /// The size of the ArrayQueue
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// Whether the ArrayQueue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        // Array to hold the items in the queue
        protected T[] items;

        /// <summary>
        /// Constructor for ArrayQueue object
        /// </summary>
        /// <param name="maxSize"></param>
        public ArrayQueue(int maxSize)
        {
            // Setting up ArrayQueue with given max size
            MaxSize = maxSize;
            items = new T[MaxSize];
        }
        
        /// <summary>
        /// Subprogram to add an item to the end of an ArrayQueue
        /// </summary>
        /// <param name="item">The item to be added</param>
        public virtual void Enqueue(T item)
        {
            // Adding item to the end of the ArrayQueue if there is room
            if (Size < MaxSize)
            {
                items[Size++] = item;
            }
        }

        /// <summary>
        /// Subprogram to remove and return the item in front of the queue
        /// </summary>
        /// <returns>The item in front of the queue</returns>
        public virtual T Dequeue()
        {
            // Caching front item - initially set to type default
            T frontItem = default(T);

            // If ArrayQueue is not empty, set front item as actual front item and remove it
            if (!IsEmpty)
            {
                frontItem = items[0];
                
                // Shifting items down
                for (int i = 1; i < Size; ++i)
                {
                    items[i - 1] = items[i];
                }

                // Decrementing size
                --Size;
            }

            return frontItem;
        }
        
        /// <summary>
        /// Subprogram to peek the item in front of the ArrayQueue
        /// </summary>
        /// <returns>The item in front of the ArrayQueue</returns>
        public virtual T Peek()
        {
            // Returing item in front of the ArrayQueue if it exists, otherwise returning the default
            return IsEmpty ? default(T) : items[0];
        }
    }
}
