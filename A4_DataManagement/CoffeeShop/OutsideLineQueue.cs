// Author: Joon Song
// File Name: OutsideLineQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Class to hold OutsideLineQueue object; inherits from IQueue and IEntity interfaces

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
    public sealed class OutsideLineQueue : IQueue<Customer>, IEntity
    {
        // List of customers in the outside line
        private List<Customer> customers = new List<Customer>();

        /// <summary>
        /// The number the customers in the outside line queue
        /// </summary>
        public int Size => customers.Count;

        /// <summary>
        /// Whether the outside line queue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Subprogram to add a customer to the end of the outside line queue
        /// </summary>
        /// <param name="customer">The customer to be added</param>
        public void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue list
            customers.Add(customer);
        }

        /// <summary>
        /// Subprogram to remove the customer in the front of the outside line queue
        /// </summary>
        /// <returns></returns>
        public Customer Dequeue()
        {
            // Caching customer in front of the queue
            Customer frontCustomer = null;

            // Adding customer in front of the line - if one exists
            if (customers.Count > 0)
            {
                frontCustomer = customers[0];
                customers.RemoveAt(0);
            }

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Update subprogram for OutsideLineQueue object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating customers in the outside line
            for (int i = 0; i < customers.Count; ++i)
            {
                customers[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Draw subprogarm for OutsideLineQueue object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing customers in the outside line
            for (int i = 0; i < customers.Count; ++i)
            {
                customers[i].Draw(spriteBatch);
            }
        }
    }
}