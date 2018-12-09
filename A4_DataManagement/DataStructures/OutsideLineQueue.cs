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
    public sealed class OutsideLineQueue : ListQueue<Customer>, IEntity
    {
        /// <summary>
        /// Subprogram to add a customer to the end of the outside line queue
        /// </summary>
        /// <param name="customer">The customer to be added</param>
        public override void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue list
            customer.AddTargetLocations(GetDestinationLocation(Size));
            base.Enqueue(customer);
        }

        /// <summary>
        /// Subprogram to remove and return the customer in the front of the outside line queue
        /// </summary>
        /// <returns>The first customer in the queue</returns>
        public override Customer Dequeue()
        {
            // Caching customer in front of the queue
            Customer frontCustomer = null;

            // Adding customer in front of the line - if one exists
            if (!IsEmpty)
            {
                frontCustomer = items[0];
                items.RemoveAt(0);

                // Shifting customers
                for (int i = 0; i < items.Count; ++i)
                {
                    items[i].AddTargetLocations(GetDestinationLocation(i));
                }
            }

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Subprogram to 'convert' the OutsideLineQueue into an array
        /// </summary>
        /// <returns>An array containing the outside line queue customers</returns>
        public Customer[] ToArray()
        {
            // Returning an array of customers
            return items.ToArray();
        }

        /// <summary>
        /// Update subprogram for OutsideLineQueue object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating customers in the outside line
            for (int i = 0; i < items.Count; ++i)
            {
                items[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Draw subprogarm for OutsideLineQueue object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing customers in the outside line
            for (int i = 0; i < items.Count; ++i)
            {
                items[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Subprogram to retrieve a given customer's destination location
        /// </summary>
        /// <param name="index">The index of the cusomer in the queue</param>
        /// <returns>The destination location of the customer</returns>
        private Vector2 GetDestinationLocation(int index)
        {
            // Returning the destination rectangle of the customer
            return new Vector2(SharedData.HORIZONTAL_BUFFER * (index + 1) - SharedData.CUSTOMER_WIDTH / 2, SharedData.VERTICAL_BUFFER + 5 * SharedData.CUSTOMER_HEIGHT);
        }
    }
}