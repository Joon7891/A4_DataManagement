// Author: Joon Song
// File Name: InsideLineQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Class to hold InsideLineQueue object; inherits from IQueue and IEntity interfaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class InsideLineQueue : IQueue<Customer>, IEntity
    {
        private const int MAX_SIZE = 16;
        private Customer[] customers = new Customer[MAX_SIZE];

        /// <summary>
        /// The number of customers in the inside line queue
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Whether the inside line queue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Subprogram to add a customer to the back of the inside line queue
        /// </summary>
        /// <param name="customer"></param>
        public void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue if there is room
            if (Size < MAX_SIZE)
            {
                customers[Size++ - 1] = customer;
            }
        }

        /// <summary>
        /// Subprogram to remove the customer in front of the inside line queue
        /// </summary>
        /// <returns></returns>
        public Customer Dequeue()
        {
            // Caching the customer in front of the line
            Customer frontCustomer = customers[0];

            // Shifting customers down the line
            for (int i = 1; i < Math.Min(Size, MAX_SIZE); ++i)
            {
                customers[i - 1] = customers[i];
            }

            // Decrementing size
            --Size;

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Update subprogram for InsideLineQueue object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating customers in the inside line
            for (int i = 0; i < Size; ++i)
            {
                customers[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Draw subprogram for InsideLineQueue object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing customers in the inside line
            for (int i = 0; i < Size; ++i)
            {
                customers[i].Draw(spriteBatch);
            }
        }
    }
}
