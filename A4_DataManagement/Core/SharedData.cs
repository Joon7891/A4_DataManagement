// Author: Joon Song
// Project Name: A4_DataManagement
// File Name: SharedData.cs
// Creation Date: 11/26/2018
// Modified Date: 11/26/2018
// Description: Class to hold a variety of shared data

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public static class SharedData
    {
        /// <summary>
        /// The height of the screen
        /// </summary>
        public const int SCREEN_HEIGHT = 600;

        /// <summary>
        /// The width of the screen
        /// </summary>
        public const int SCREEN_WIDTH = 1200;
        
        /// <summary>
        /// The height of the coffee shop
        /// </summary>
        public const int COFFEE_SHOP_HEIGHT = 600;

        /// <summary>
        /// The width of the coffee shop
        /// </summary>
        public const int COFFEE_SHOP_WIDTH = 800;

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random RNG { get; private set; }

        /// <summary>
        /// The width of the customer
        /// </summary>
        public const int CUSTOMER_WIDTH = 52;

        /// <summary>
        /// The height of the customer
        /// </summary>
        public const int CUSTOMER_HEIGHT = 72;

        /// <summary>
        /// The vertical buffer of the customer's location
        /// </summary>
        public const int VERTICAL_BUFFER = 150;

        /// <summary>
        /// The horizontal buffer of the customer's location
        /// </summary>
        public const int HORIZONTAL_BUFFER = 100;

        /// <summary>
        /// Static constructor to initialize SharedData class data
        /// </summary>
        static SharedData()
        {
            // Constructing random number generator
            RNG = new Random();
        }
    }
}
