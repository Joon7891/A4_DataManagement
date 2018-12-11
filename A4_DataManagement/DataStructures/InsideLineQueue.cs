// Author: Joon Song
// File Name: InsideLineQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Class to hold InsideLineQueue object; inherits from ArrayQueue and IEntity

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
    public sealed class InsideLineQueue : ArrayQueue<Customer>, IEntity
    {
        // Customer-related variables
        private const int MAX_SIZE = 16;
        private static Vector2[] customerLocations = new Vector2[MAX_SIZE];

        /// <summary>
        /// Static constructor to setup InsideLineQueue class
        /// </summary>
        static InsideLineQueue()
        {
            // Setting up customer locations
            customerLocations[0] = new Vector2((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 - 3 * SharedData.HORIZONTAL_SPACING, SharedData.VERTICAL_BUFFER + SharedData.VERTICAL_SPACING);
            customerLocations[8] = new Vector2(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - SharedData.HORIZONTAL_SPACING, SharedData.VERTICAL_BUFFER + 4 * SharedData.VERTICAL_SPACING);
            for (int i = 0; i < 7; ++i)
            {
                customerLocations[i + 1] = new Vector2((SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH) / 2 + SharedData.HORIZONTAL_SPACING * (i - 3), SharedData.VERTICAL_BUFFER + 3 * SharedData.VERTICAL_SPACING);
                customerLocations[i + 9] = new Vector2(SharedData.COFFEE_SHOP_WIDTH - SharedData.CUSTOMER_WIDTH / 2 - SharedData.HORIZONTAL_SPACING * (i + 1), SharedData.VERTICAL_BUFFER + 5 * SharedData.VERTICAL_SPACING);
            }
        }

        /// <summary>
        /// Constructor for InsideLineQueue
        /// </summary>
        public InsideLineQueue() : base(MAX_SIZE)
        {
            // Constructor is empty as only base constructor needs to be called
        }

        /// <summary>
        /// Subprogram to add a customer to the back of the inside line queue
        /// </summary>
        /// <param name="customer">The customer to be added to the end</param>
        public override void Enqueue(Customer customer)
        {
            // Adding customer to the end of the queue if there is room
            if (Size < MAX_SIZE)
            {
                customer.AddTargetLocations(ArrayHelper<Vector2>.GetSubarray(customerLocations.Reverse().ToArray(), 0, MAX_SIZE - Size));
                items[Size++] = customer;
            }
        }

        /// <summary>
        /// Subprogram to remove and return the customer in front of the inside line queue
        /// </summary>
        /// <returns>The first customer in the queue</returns>
        public override Customer Dequeue()
        {
            // Caching the customer in front of the line
            Customer frontCustomer = items[0];

            // Shifting customers down the line
            for (int i = 1; i < Size; ++i)
            {
                items[i].AddTargetLocations(customerLocations[i - 1]);
                items[i - 1] = items[i];
            }

            // Decrementing size
            --Size;

            // Returning customer in front of the line
            return frontCustomer;
        }

        /// <summary>
        /// Subprogram to 'convert' the InsideLineQueue into an array
        /// </summary>
        /// <returns>An array containing the inside line queue customers</returns>
        public Customer[] ToArray()
        {
            // Returning an array of customers in the inside line queue
            return ArrayHelper<Customer>.GetSubarray(items, 0, Size);                
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
                items[i].Update(gameTime);
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
                items[i].Draw(spriteBatch);
            }
        }
    }
}
