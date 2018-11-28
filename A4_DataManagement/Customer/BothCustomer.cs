// Author: Joon Song
// File Name: BothCustomer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/27/2018
// Modified Date: 11/29/2018
// Description: Class to hold BothCustomer object; child to Customer object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public sealed class BothCustomer : Customer
    {
        // Static variables to hold BothCustomer service time and instances count
        private const int SERVICE_TIME = 30;
        private static int instancesCounter = 0;

        /// <summary>
        /// Constructor for BothCustomer object
        /// </summary>
        public BothCustomer()
        {
            // Setting up customer service time and name
            serviceTime = SERVICE_TIME;
            Name = $"Both{++instancesCounter}";
        }
    }
}
