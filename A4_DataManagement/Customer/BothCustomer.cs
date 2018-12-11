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
        // Constant for BothCustomer service time
        private const int SERVICE_TIME = 30;

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
                    directionalImages[(int)direction, i] = Main.Content.Load<Texture2D>
                        ($"Images/Sprites/Customers/BothCustomer/bothCustomer{direction.ToString()}{i}");
                }
            }
        }

        /// <summary>
        /// Constructor for BothCustomer object
        /// </summary>
        /// <param name="ID">The unique ID identifier of the BothCustomer</param>
        public BothCustomer(int ID) : base()
        {
            // Setting up various both customer attributes
            timeToServe = SERVICE_TIME;
            Name = $"Both{ID}";
            base.directionalImages = directionalImages;
            SetupHeader();
        }
    }
}
