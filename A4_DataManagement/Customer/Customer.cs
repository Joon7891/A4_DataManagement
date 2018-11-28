// Author: Joon Song
// File Name: Customer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 12/10/2018
// Description: Class to hold Customer object

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public sealed class Customer
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The current wait time of the customer
        /// </summary>
        public double WaitTime => Math.Round(waitTime, 2);

        /// <summary>
        /// Whether the customer has been serviced yet
        /// </summary>
        public bool Serviced => serviceTime <= 0;

        /// <summary>
        /// Whether the customer is currently moving
        /// </summary>
        public bool IsMoving => !(rectangle.X == targetRectangle.X && rectangle.Y == targetRectangle.Y);

        // Time related variables
        private double waitTime = 0;
        private double serviceTime;

        // Customer image, rectangle, and movement related variables
        private static Texture2D image;
        private Rectangle rectangle;
        private Rectangle targetRectangle = new Rectangle();
        private double nonRoundedX;
        private double nonRoundedY;
        private Vector2 velocity = new Vector2();

        // Hashmap for various various service times and counts for each type of customer 
        private static Dictionary<CustomerType, int> customerServiceTimes = new Dictionary<CustomerType, int>();
        private static Dictionary<CustomerType, int> customerCounts = new Dictionary<CustomerType, int>();

        /// <summary>
        /// Static constructor to set up various Customer components
        /// </summary>
        static Customer()
        {
            // Setting up customer service times hashmap
            customerCounts.Add(CustomerType.Coffee, 12);
            customerCounts.Add(CustomerType.Food, 18);
            customerCounts.Add(CustomerType.Both, 30);

            // Setting up customer counts hashmap
            for (CustomerType customerType = CustomerType.Coffee; customerType <= CustomerType.Both; ++customerType)
            {
                customerServiceTimes.Add(customerType, 0);
            }
        }

        /// <summary>
        /// Constructor for Customer object
        /// </summary>
        public Customer()
        {
            // TO DO: Cache CustomerType?

            // Randomly generating customer type and setting appropraite data
            CustomerType customerType = (CustomerType)SharedData.RNG.Next(0, 3);
            serviceTime = customerServiceTimes[customerType];
            Name = $"{customerType.ToString()}{++customerCounts[customerType]}";
        }

        /// <summary>
        /// Update subprogram for Customer object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Increasing wait time
            waitTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0;
            

            // Moving customer if they are not at their target location
            if (IsMoving)
            {
                Move(gameTime);
            }
        }

        /// <summary>
        /// Draw subprogram for Customer object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // TO DO: Add Customer Sprites
        }

        /// <summary>
        /// Subprogram to 'serve' the customer
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Serve(GameTime gameTime)
        {
            // Decreasing service time
            serviceTime -= gameTime.ElapsedGameTime.Milliseconds / 1000.0;
        }

        /// <summary>
        /// Subprogram to move the customer to its target location
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timinig value</param>
        private void Move(GameTime gameTime)
        {          
            // Moving customer
            nonRoundedX += velocity.X * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
            nonRoundedY += velocity.Y * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
            rectangle.X = (int)(nonRoundedX + 0.5);
            rectangle.Y = (int)(nonRoundedY + 0.5);

            // Adjusting x-velocity if it will overshoot
            if (Math.Abs(rectangle.X - targetRectangle.X) * 60 < Math.Abs(velocity.X))
            {
                velocity.X = (targetRectangle.X - rectangle.X) * 60;
            }

            // Adjusting y-velocity if will overshoot
            if (Math.Abs(rectangle.Y - targetRectangle.Y) * 60 < Math.Abs(velocity.Y))
            {
                velocity.Y = (targetRectangle.Y - rectangle.Y) * 60;
            }
        }

        /// <summary>
        /// Subprogram to set the movement of a Customer
        /// </summary>
        /// <param name="targetRectangle">The target rectangle Customer is to move to</param>
        /// <param name="movementTime">The time in which Customer is to move</param>
        public void SetMovement(Rectangle targetRectangle, float movementTime)
        {
            // Setting up velocity vector
            velocity.X = (targetRectangle.X - rectangle.X) / movementTime;
            velocity.Y = (targetRectangle.Y - rectangle.Y) / movementTime;

            // Setting up various to ensure proper correlation of movement
            nonRoundedX = rectangle.X;
            nonRoundedY = rectangle.Y;
            this.targetRectangle = targetRectangle;
        }
    }
}