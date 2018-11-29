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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class BothCustomer : Customer
    {
        // Static variables to hold BothCustomer service time and instances count
        private const int SERVICE_TIME = 30;
        private static int instancesCounter = 0;

        // 2D array to hold directional images for FoodCustomer
        private new static Texture2D[,] directionalImages = new Texture2D[4, 3];

        /// <summary>
        /// Static constuctor to set up various BothCustomer components
        /// </summary>
        static BothCustomer()
        {
            // Importing directional images for CoffeeCustomer
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (int i = 0; i < directionalImages.GetLength(1); ++i)
                {
                    directionalImages[(int)direction, i] = CoffeeShopSimulation.Content.Load<Texture2D>
                        ($"Images/Sprites/Customers/BothCustomer/bothCustomer{direction.ToString()}{i}");
                }
            }
        }

        /// <summary>
        /// Constructor for BothCustomer object
        /// </summary>
        public BothCustomer() : base()
        {
            // Setting up various both customer attributes
            serviceTime = SERVICE_TIME;
            Name = $"Both{++instancesCounter}";
            base.directionalImages = directionalImages;
        }
    }
}
