// Author: Joon Song
// File Name: ServiceLine.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 

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
        private const int CASHER_NUM = 4;
        private Customer[] customers = new Customer[CASHER_NUM];
        private List<Customer> exitingCustomers = new List<Customer>();

        private static Rectangle[] cashierRectangles = new Rectangle[CASHER_NUM];
        private static Rectangle exitRectangle;

        public int SpotsAvailable => customers.Count(customer => customer == null);

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
            for (int i = 0; i < CASHER_NUM; ++i)
            {
                if (customers[i] != null)
                {
                    if (!customers[i].IsMoving)
                    {
                        if (customers[i].Serviced)
                        {
                            exitingCustomers.Add(customers[i]);
                            customers[i] = null;
                        }
                        else
                        {
                            customers[i].Serve(gameTime);
                        }
                    }
                }
            }
        }

        public void Add(Customer customer)
        {
            for (byte i = 0; i < CASHER_NUM; ++i)
            {
                if (customers[i] == null)
                {
                    customers[i] = customer;
                }
            }
        }
    }
}
