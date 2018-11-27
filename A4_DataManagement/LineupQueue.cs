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
        private List<Customer> lineupCustomers = new List<Customer>();
        private double[] customerWaitTimes;

        private const int NEW_CUSTOMER_TIME = 3;
        private double newCustomerTimer = 0;
        private int newCustomerType;
        private int[] customerTypesCounter = new int[3];

        public void Update(GameTime gameTime)
        {
            // Updating customer timer and adding new customer - if appropriate
            newCustomerTimer += gameTime.ElapsedGameTime.Milliseconds / 1000.0;
            if (newCustomerTimer >= NEW_CUSTOMER_TIME)
            {
                newCustomerTimer -= newCustomerTimer;
                newCustomerType = SharedData.RNG.Next(0, 3);
                ++customerTypesCounter[newCustomerType];
            }

            // 
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing each customer
            for (int i = 0; i < lineupCustomers.Count; ++i)
            {
                lineupCustomers[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Subprogram to obtain the top five wait times
        /// </summary>
        /// <returns>The top five wait times formatted in a string</returns>
        public string GetTopFiveWaitTimes()
        {
            string topFiveWaitTimes = string.Empty;
            Customer[] customersByWaitTime = GetSortedWaitTimes();

            for (int i = 0; i < customersByWaitTime.Length; ++i)
            {
                topFiveWaitTimes += $"{i + 1}. {customersByWaitTime[i].Name} - {customersByWaitTime[i].WaitTime}\n";
            }

            // Returning top five wait times formatted as a string
            return topFiveWaitTimes;
        }

        /// <summary>
        /// Subprogram to obtain the sorted wait times for the customers
        /// </summary>
        /// <returns>An array of customers, sorted by wait time - decreasing</returns>
        private Customer[] GetSortedWaitTimes()
        {
            // Calling MergeSort subprogram and returning sorted array
            return SortHelper<Customer>.MergeSort(customers.ToArray(), (a, b) => a.WaitTime >= b.WaitTime);
        }

        public void Enqueue(Customer customer)
        {

        }
        
        public Customer 
    }
}