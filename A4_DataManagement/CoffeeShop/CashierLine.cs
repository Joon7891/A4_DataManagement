// Author: Joon Song
// File Name: ServiceLine.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 12/04/2018
// Description: Class to hold ServiceLine object; inherits from IEntity object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class CashierLine : IEntity
    {
        // Customer related data
        private const int NUM_CASHIER = 4;
        private Customer[] cashierCustomers = new Customer[NUM_CASHIER];
        private List<Customer> exitingCustomers = new List<Customer>();

        // Various rectangles that customers are to be in
        private static Rectangle[] exitRectangles = new Rectangle[NUM_CASHIER];
        private static Rectangle[] cashierRectangles = new Rectangle[NUM_CASHIER];

        /// <summary>
        /// The number of cashiers currently available
        /// </summary>
        public int CashiersAvailable => cashierCustomers.Count(customer => customer == null);

        /// <summary>
        /// The number of customers in the cashier line - those being served and those exiting
        /// </summary>
        public int Count => NUM_CASHIER - CashiersAvailable + exitingCustomers.Count;

        /// <summary>
        /// The total number of people served
        /// </summary>
        public int TotalServed { get; private set; }

        /// <summary>
        /// The maximum wait time for a customer
        /// </summary>
        public double MaxWaitTime { get; private set; }

        /// <summary>
        /// The minimum wait time for a customer
        /// </summary>
        public double MinWaitTime { get; private set; }

        /// <summary>
        /// The average wait time for a customer, rounded to two deciminal places
        /// </summary>
        public double AverageWaitTime => Math.Round(averageWaitTime, 2);

        // Average wait time for a customer
        private double averageWaitTime;

        /// <summary>
        /// Static constructor to set up various CashierLine object properties
        /// </summary>
        static CashierLine()
        {
            // Setting up customer rectangles
            for (int i = 0; i < NUM_CASHIER; ++i)
            {
                cashierRectangles[i] = new Rectangle(74 + 200 * i, SharedData.VERTICAL_BUFFER - SharedData.CUSTOMER_HEIGHT, 
                    SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
                exitRectangles[i] = new Rectangle(74 + (i % 2 == 0 ? -1 : 1) * SharedData.CUSTOMER_WIDTH + 200 * i, 
                    -SharedData.CUSTOMER_HEIGHT, SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            }
        }

        /// <summary>
        /// Subprogram to 'convert' CashierLine into an array
        /// </summary>
        /// <returns>An array containing the cashier line customers</returns>
        public Customer[] ToArray()
        {
            // Returning the combination of customer with a cashier and those exiting
            return ArrayHelper<Customer>.Combine(cashierCustomers.Where(customer => customer != null).ToArray(), exitingCustomers.ToArray());
        }

        /// <summary>
        /// Update subprogram for CashierLine object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {            
            // Iterating through each cashier customer
            for (int i = 0; i < NUM_CASHIER; ++i)
            {
                if (cashierCustomers[i] != null)
                {
                    // Updating customer
                    cashierCustomers[i].Update(gameTime);

                    // Serving customers at the cashier
                    if (!cashierCustomers[i].IsMoving)
                    {
                        cashierCustomers[i].Serve(gameTime);
                    }

                    // Making customer exit if they have been servied
                    if (cashierCustomers[i].Serviced)
                    {
                        cashierCustomers[i].SetMovement(exitRectangles[i]);
                        exitingCustomers.Add(cashierCustomers[i]);
                        cashierCustomers[i] = null;
                    }
                }
            }

            // Updating exiting customers
            for (int i = 0; i < exitingCustomers.Count; ++i)
            {
                exitingCustomers[i].Update(gameTime);

                // Removing exiting customer if they are off screen
                if (!exitingCustomers[i].IsMoving)
                {
                    // Updating max wait time if customer's wait time is a new max
                    if (exitingCustomers[i].WaitTime > MaxWaitTime)
                    {
                        MaxWaitTime = exitingCustomers[i].WaitTime;
                    }

                    // Updating min wait time if customer's wait time is a new min
                    if (exitingCustomers[i].WaitTime < MinWaitTime || MinWaitTime == default(int))
                    {
                        MinWaitTime = exitingCustomers[i].WaitTime;
                    }

                    // Updating avergae wait time
                    averageWaitTime = (exitingCustomers[i].WaitTime + averageWaitTime * TotalServed++) / TotalServed;

                    // Removing customer
                    exitingCustomers.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        /// Draw subprogram for CashierLine object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing customers with cashiers
            for (int i = 0; i < NUM_CASHIER; ++i)
            {
                cashierCustomers[i]?.Draw(spriteBatch);
            }

            // Drawing exiting customers
            for (int i = 0; i < exitingCustomers.Count; ++i)
            {
                exitingCustomers[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Subprogram to add a customer to the cashier line
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            // Determining opening and adding customer as appropriate
            for (int i = 0; i < NUM_CASHIER; ++i)
            {
                if (cashierCustomers[i] == null)
                {
                    cashierCustomers[i] = customer;
                    cashierCustomers[i].SetMovement(cashierRectangles[i]);
                    return;
                }
            }
        }
    }
}
