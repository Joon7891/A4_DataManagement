// Author: Joon Song
// File Name: LineupQueue.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 12/01/2018
// Description: Class to hold LineupQueue object

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
    public sealed class LineupQueue
    {
        private const byte INSIDE_LINE_SIZE = 16;
        private Customer[] insideCustomers = new Customer[INSIDE_LINE_SIZE];
        private List<Customer> outsideCustomers = new List<Customer>();

        /// <summary>
        /// The count of the line up
        /// </summary>
        public int Count { get; private set; }
        
        public void Update(GameTime gameTime)
        {
            // Updating customers for inside line up
            for (byte i = 0; i < Math.Min(Count, INSIDE_LINE_SIZE); ++i)
            {
                insideCustomers[i].Update(gameTime);
            }

            // Updating customers for outside line up
            for (int i = 0; i < outsideCustomers.Count; ++i)
            {
                outsideCustomers[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }


        public void Enqueue(Customer customer)
        {
            if (Count < INSIDE_LINE_SIZE)
            {
                insideCustomers[Count++] = customer;
            }
            else
            {
                outsideCustomers.Add(customer);
                ++Count;
            }
        }


        /// <summary>
        /// Subprogram to dequeue the first customer in the line
        /// </summary>
        /// <returns>The first customer in the line</returns>
        public Customer Dequeue()
        {
            // Caching first customer in the line
            Customer firstCustomer = insideCustomers[0];
            
            // Proceding it there are customers in LineupQueue
            if (Count > 0)
            {
                // Shifting customers down line
                for (byte i = 1; i < Math.Min(Count, INSIDE_LINE_SIZE); ++i)
                {
                    insideCustomers[i - 1] = insideCustomers[i];
                }

                // Moving customer from outside of the liine to the inside
                if (outsideCustomers.Count > 0)
                {
                    insideCustomers[INSIDE_LINE_SIZE - 1] = outsideCustomers[0];
                    outsideCustomers.RemoveAt(0);
                }

                // Decrementing count
                --Count;
            }

            // Returning first customer in the line
            return firstCustomer;
        }
    }
}