// Author: Joon Song
// File Name: CoffeeCustomer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 11/29/2018
// Description: Class to hold CoffeeCustomer object; child to Customer object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public sealed class CoffeeCustomer : Customer
    {
        // Static variables to hold CoffeeCustomer service time and instances count
        private const int SERVICE_TIME = 12;
        private static int instancesCounter = 0;

        /// <summary>
        /// Constructor for CoffeeCustomer object
        /// </summary>
        public CoffeeCustomer()
        {
            // Setting customer service time and name
            serviceTime = SERVICE_TIME;
            Name = $"Coffee{++instancesCounter}";
        }

    }
}
