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
            customer.SetMovement(GetDestinationLocation(Size));
            customers.Add(customer);
        }

        /// <summary>
        /// Subprogram to remove and return the customer in the front of the outside line queue
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

                // Shifting customers
                for (int i = 0; i < customers.Count; ++i)
                {
                    customers[i].SetMovement(GetDestinationLocation(i));
                }
            }

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Subprogram to return the customer in front of the outside line queue
        /// </summary>
        /// <returns>The customer in front of the outside line queue</returns>
        public Customer Peek()
        {
            // Returning the customer in front of the line - if one exists
            if (Size > 0)
            {
                return customers[0];
            }

            // Otherwise returning null
            return null;
        }

        /// <summary>
        /// Subprogram to 'convert' the OutsideLineQueue into an array
        /// </summary>
        /// <returns>An array containing the outside line queue customers</returns>
        public Customer[] ToArray()
        {
            // Returning an array of customers
            return customers.ToArray();
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

        /// <summary>
        /// Subprogram to retrieve a given customer's destination location
        /// </summary>
        /// <param name="index">The index of the cusomer in the queue</param>
        /// <returns>The destination location of the customer</returns>
        private Vector2 GetDestinationLocation(int index)
        {
            // If
            if (index == 0)
            {
                return new Vector2(100 - SharedData.CUSTOMER_WIDTH / 2, SharedData.VERTICAL_BUFFER + 4 * SharedData.CUSTOMER_HEIGHT);
            }
            else
            {
                return new Vector2(100 * index - SharedData.CUSTOMER_WIDTH / 2, SharedData.VERTICAL_BUFFER + 5 * SharedData.CUSTOMER_HEIGHT);
            }
        }
    }
}