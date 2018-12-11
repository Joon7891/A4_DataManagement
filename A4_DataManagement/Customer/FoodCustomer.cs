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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class FoodCustomer : Customer
    {
        // Constant for FoodCustomer service time
        private const int SERVICE_TIME = 18;

        // 2D array to hold directional images for FoodCustomer
        private new static Texture2D[,] directionalImages = new Texture2D[4, 3];

        /// <summary>
        /// Static constuctor to set up various FoodCustomer components
        /// </summary>
        static FoodCustomer()
        {
            // Importing directional images for FoodCustomer
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (int i = 0; i < directionalImages.GetLength(1); ++i)
                {
                    directionalImages[(int)direction, i] = Main.Content.Load<Texture2D>
                        ($"Images/Sprites/Customers/FoodCustomer/foodCustomer{direction.ToString()}{i}");
                }
            }
        }

        /// <summary>
        /// Constructor for FoodCustomer object
        /// </summary>
        /// <param name="ID">The unique ID identifier of the FoodCustomer</param>
        public FoodCustomer(int ID) : base()
        {
            // Setting up various food customer attributes
            timeToServe = SERVICE_TIME;
            Name = $"Food{ID}";
            base.directionalImages = directionalImages;
            SetupHeader();
        }
    }
}
