// Author: Joon Song
// File Name: ServiceLine.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 12/04/2018
// Description: Class to hold ServiceLine object

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
    public sealed class ServiceLine
    {
        private const int CASHIER_NUM = 4;
        private Customer[] cashierCustomers = new Customer[CASHIER_NUM];
        private List<Customer> exitingCustomers = new List<Customer>();

        private static Rectangle[] cashierRectangles = new Rectangle[CASHIER_NUM];
        private static Rectangle exitRectangle;

        public int SpotsAvailable => cashierCustomers.Count(customer => customer == null);

        public int TotalCount => (CASHIER_NUM - SpotsAvailable) + exitingCustomers.Count;

        static ServiceLine()
        {
            for (int i = 0; i < cashierRectangles.Length; ++i)
            {
                cashierRectangles[i] = new Rectangle(240 + 80 * i, 100, 40, 40);
            }
            exitRectangle = new Rectangle(-40, 100, 40, 40);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < CASHIER_NUM; ++i)
            {
                if (cashierCustomers[i] != null)
                {
                    if (!cashierCustomers[i].IsMoving)
                    {
                        if (cashierCustomers[i].Serviced)
                        {
                            cashierCustomers[i].SetMovement(exitRectangle, 0.5f * i);
                            exitingCustomers.Add(cashierCustomers[i]);
                            cashierCustomers[i] = null;
                        }
                        else
                        {
                            cashierCustomers[i].Serve(gameTime);
                        }
                    }
                }
            }

            for (int i = 0; i < exitingCustomers.Count; ++i)
            {
                exitingCustomers[i].Update(gameTime);

                if (!exitingCustomers[i].IsMoving)
                {
                    exitingCustomers.RemoveAt(i);
                    --i;
                }
            }
        }

        public void Add(Customer customer)
        {
            for (byte i = 0; i < CASHIER_NUM; ++i)
            {
                if (cashierCustomers[i] == null)
                {
                    cashierCustomers[i] = customer;
                    break;
                }
            }
        }
    }
}
