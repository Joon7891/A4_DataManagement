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
        private static Rectangle[] customerRectangles = new Rectangle[MAX_SIZE];

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
            // Setting up customer rectangles
            for (int i = 0; i < MAX_SIZE; ++i)
            {
                customerRectangles[i] = GetCustomerRectangle(i);
            }
        }

        /// <summary>
        /// Subprogram to generate the customer rectangle
        /// </summary>
        /// <param name="index">The index of the customer rectangle</param>
        /// <returns>The customer rectangle</returns>
        private static Rectangle GetCustomerRectangle(int index)
        {
            if (index == 0)
            {
                return new Rectangle((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 - 300, SharedData.VERTICAL_BUFFER, 
                    SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            }
            else if (index < 8)
            {
                return new Rectangle((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 + 100 * (index - 4),
                    SharedData.VERTICAL_BUFFER + SharedData.CUSTOMER_HEIGHT, SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            }
            else if (index == 8)
            {
                return new Rectangle(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - 100, SharedData.VERTICAL_BUFFER + 2 * SharedData.CUSTOMER_HEIGHT,
                    SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            }
            else
            {
                return new Rectangle(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - 100 * (index - 10), SharedData.VERTICAL_BUFFER + 3 * SharedData.CUSTOMER_HEIGHT,
                    SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            }
        }

        /// <summary>
        /// Subprogram to add a customer to the back of the inside line queue
        /// </summary>
        /// <param name="customer"></param>
        public void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue if there is room
            if (Size < MAX_SIZE)
            {
                customer.SetMovement(customerRectangles[Size]);
                customers[Size++] = customer;
            }
        }

        /// <summary>
        /// Subprogram to remove and return the customer in front of the inside line queue
        /// </summary>
        /// <returns></returns>
        public Customer Dequeue()
        {
            // Caching the customer in front of the line
            Customer frontCustomer = customers[0];

            // Shifting customers down the line
            for (int i = 1; i < Math.Min(Size, MAX_SIZE); ++i)
            {
                customers[i].SetMovement(customerRectangles[i - 1]);
                customers[i - 1] = customers[i];
            }
            customers[Size - 1] = null;

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
            // Returning an array of non-null customers
            return customers.Where(customer => customer != null).ToArray();
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
