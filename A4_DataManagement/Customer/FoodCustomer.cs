// Author: Joon Song
// File Name: FoodCustomer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 11/29/2018
// Description: Class to hold FoodCustomer object; child to Customer object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public sealed class FoodCustomer : Customer
    {
        // Static variables to hold FoodCustomer service time and instances count
        private const int SERVICE_TIME = 18;
        private static int instancesCounter = 0;

        /// <summary>
        /// Constructor for FoodCustomer object
        /// </summary>
        public FoodCustomer()
        {
            // Setting up customer service time and name
            serviceTime = SERVICE_TIME;
            Name = $"Food{++instancesCounter}";
        }
    }
}
