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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class CoffeeCustomer : Customer
    {
        // Static variables to hold CoffeeCustomer service time and instances count
        private const int SERVICE_TIME = 12;
        private static int instancesCounter = 0;

        // 2D array to hold directional images for CoffeeCustomer
        private new static Texture2D[,] directionalImages = new Texture2D[4, 3];

        /// <summary>
        /// Static constuctor to set up various CoffeeCustomer components
        /// </summary>
        static CoffeeCustomer()
        {
            // Importing directional images for CoffeeCustomer
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (int i = 0; i < directionalImages.GetLength(1); ++i)
                {
                    directionalImages[(int)direction, i] = Main.Content.Load<Texture2D>
                        ($"Images/Sprites/Customers/CoffeeCustomer/coffeeCustomer{direction.ToString()}{i}");
                }
            }
        }

        /// <summary>
        /// Constructor for CoffeeCustomer object
        /// </summary>
        public CoffeeCustomer() : base()
        {
            // Setting up various coffee customer attributes
            serviceTime = SERVICE_TIME;
            Name = $"Coffee{++instancesCounter}";
            base.directionalImages = directionalImages;
        }
    }
}
