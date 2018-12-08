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
        // Customer-related variables
        private const int MAX_SIZE = 16;
        private Customer[] customers = new Customer[MAX_SIZE];
        private static Vector2[] customerLocations = new Vector2[MAX_SIZE];

        /// <summary>
        /// The number of customers in the inside line queue
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Whether the inside line queue is empty
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Static constructor to setup InsideLineQueue class
        /// </summary>
        static InsideLineQueue()
        {
            // Setting up customer locations
            for (int i = 0; i < MAX_SIZE; ++i)
            {
                customerLocations[i] = GetCustomerLocation(i);
            }
        }

        /// <summary>
        /// Subprogram to generate the customer location
        /// </summary>
        /// <param name="index">The index of the customer location</param>
        /// <returns>The customer location</returns>
        private static Vector2 GetCustomerLocation(int index)
        {
            // Returning appropriate customer vector based on index
            if (index == 0)
            {
                return new Vector2((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 - 3 * SharedData.HORIZONTAL_BUFFER, SharedData.VERTICAL_BUFFER);
            }
            else if (index < 8)
            {
                return new Vector2((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 + SharedData.HORIZONTAL_BUFFER * (index - 4), SharedData.VERTICAL_BUFFER + SharedData.CUSTOMER_HEIGHT);
            }
            else if (index == 8)
            {
                return new Vector2(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - SharedData.HORIZONTAL_BUFFER, SharedData.VERTICAL_BUFFER + 2 * SharedData.CUSTOMER_HEIGHT);
            }
            else
            {
                return new Vector2(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - SharedData.HORIZONTAL_BUFFER * (index - 8), SharedData.VERTICAL_BUFFER + 3 * SharedData.CUSTOMER_HEIGHT);
            }
        }

        /// <summary>
        /// Subprogram to add a customer to the back of the inside line queue
        /// </summary>
        /// <param name="customer">The customer to be added to the end</param>
        public void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue if there is room
            if (Size < MAX_SIZE)
            {
                customer.AddTargetLocations(ArrayHelper<Vector2>.GetSubarray(customerLocations.Reverse().ToArray(), 0, MAX_SIZE - Size));
                customers[Size++] = customer;
            }
        }

        /// <summary>
        /// Subprogram to remove and return the customer in front of the inside line queue
        /// </summary>
        /// <returns>The first customer in the queue</returns>
        public Customer Dequeue()
        {
            // Caching the customer in front of the line
            Customer frontCustomer = customers[0];

            // Shifting customers down the line
            for (int i = 1; i < Math.Min(Size, MAX_SIZE); ++i)
            {
                customers[i].AddTargetLocations(customerLocations[i - 1]);
                customers[i - 1] = customers[i];
            }

            // Decrementing size
            --Size;

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Subprogram to return the customer in front of the inside line queue
        /// </summary>
        /// <returns>The customer in front of the inside line</returns>
        public Customer Peek()
        {
            // Returning customer in front of the line if one exists
            if (Size > 0)
            {
                return customers[0];
            }

            // Otherwise returning null
            return null;
        }

        /// <summary>
        /// Subprogram to 'convert' the InsideLineQueue into an array
        /// </summary>
        /// <returns>An array containing the inside line queue customers</returns>
        public Customer[] ToArray()
        {
            // Returning an array of customers in the inside line queue
            return ArrayHelper<Customer>.GetSubarray(customers, 0, Size);                
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
