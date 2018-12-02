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

        // Memo/memoization of destination customer rectangles
        private static Dictionary<int, Rectangle> destinationRectangleMemo = new Dictionary<int, Rectangle>();

        /// <summary>
        /// The number the customers in the outside line queue
        /// </summary>
        public int Size => customers.Count;

        /// <summary>
        /// Whether the outside line queue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Static constructor for OutsideLineQueue object
        /// </summary>
        static OutsideLineQueue()
        {
            // Adding first destination rectangle to memo
            destinationRectangleMemo.Add(0, new Rectangle(100 - SharedData.CUSTOMER_WIDTH / 2,
                SharedData.VERTICAL_BUFFER + 4 * SharedData.CUSTOMER_HEIGHT, SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT));
        }

        /// <summary>
        /// Subprogram to add a customer to the end of the outside line queue
        /// </summary>
        /// <param name="customer">The customer to be added</param>
        public void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue list
            customer.SetMovement(GetDestinationRectangle(Size));
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
                    customers[i].SetMovement(GetDestinationRectangle(i));
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
        /// Subprogram to retrieve a given customer's destination rectangle
        /// </summary>
        /// <param name="index">The index of the cusomer in the queue</param>
        /// <returns>The destination rectangle of the customer</returns>
        private Rectangle GetDestinationRectangle(int index)
        {
            // Instance of destination rectangle as cache
            Rectangle destinationRectangle;
            
            // Returning destination rectangle if it's in memo - utilizing memoization
            if (destinationRectangleMemo.ContainsKey(index))
            {
                return destinationRectangleMemo[index];
            }
            
            // Otherwise constructing destination rectangle, memoizing it and returning it
            destinationRectangle = new Rectangle(100 * index - SharedData.CUSTOMER_WIDTH / 2, SharedData.VERTICAL_BUFFER +
                5 * SharedData.CUSTOMER_HEIGHT, SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            destinationRectangleMemo.Add(index, destinationRectangle);
            return destinationRectangleMemo[index];
        }
    }
}