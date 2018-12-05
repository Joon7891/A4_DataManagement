// Author: Joon Song
// File Name: Customer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 12/10/2018
// Description: Class to hold Customer object; inherits from IEntity interface

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace A4_DataManagement
{
    public abstract class Customer : IEntity
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; protected set; }

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
        public bool IsMoving => !(rectangle.X == targetLocation.X && rectangle.Y == targetLocation.Y);

        // Time and service related variables
        private double waitTime = 0;
        protected double serviceTime;
        private static SoundEffect serveSoundEffect;

        // Graphics related-data
        private Direction currentDirection = Direction.Left;
        protected Texture2D[,] directionalImages;
        protected Texture2D currentImage;
        private int frameCounter = 1;
        private int imageNumber = 0;

        // Movement related variables
        private Rectangle rectangle;
        private Vector2 targetLocation;
        private double nonRoundedX;
        private double nonRoundedY;
        private Vector2 velocity;
        
        /// <summary>
        /// Static constructor to set up Customer class
        /// </summary>
        static Customer()
        {
            // Loading sound effects
            serveSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/serveSoundEffect");
        }

        /// <summary>
        /// Constructor for Customer object
        /// </summary>
        public Customer()
        {
            // Constructing initial customer rectangle
            rectangle = new Rectangle(SharedData.COFFEE_SHOP_WIDTH + SharedData.CUSTOMER_WIDTH, SharedData.VERTICAL_BUFFER + 5 * SharedData.CUSTOMER_HEIGHT,
                SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
        }

        /// <summary>
        /// Update subprogram for Customer object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Increasing wait time
            waitTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // Updating customer image
            currentImage = directionalImages[(int)currentDirection, IsMoving ? imageNumber : 1];

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
            // Drawing customer
            spriteBatch.Draw(currentImage, rectangle, Color.White);
        }

        /// <summary>
        /// Subprogram to 'serve' the customer
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Serve(GameTime gameTime)
        {
            // Decreasing service time
            serviceTime -= gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // Playing served sound effect if fully served
            if (serviceTime <= 0)
            {
                serveSoundEffect.CreateInstance().Play();
            }
        }

        /// <summary>
        /// Subprogram to move the customer to its target location
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timinig value</param>
        private void Move(GameTime gameTime)
        {
            // Updating image frame related data
            frameCounter = (frameCounter + 1) % 10;
            if (frameCounter == 0)
            {
                imageNumber = (imageNumber + 1) % 3;
            }

            // Moving customer in appropraite direction
            if (currentDirection == Direction.Left || currentDirection == Direction.Right)
            {
                // Adjusting x-velocity and flipping direction if x-movement will overshoot
                if (Math.Abs(rectangle.X - targetLocation.X) * 60 < Math.Abs(velocity.X))
                {
                    velocity.X = (targetLocation.X - rectangle.X) * 60;
                    currentDirection = velocity.Y > 0 ? Direction.Down : Direction.Up;
                }

                // Moving customer rectangle
                nonRoundedX += velocity.X * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
                rectangle.X = (int)(nonRoundedX + 0.5);
            }
            else
            {
                // Adjusting y-velocity if y-movement will overshoot
                if (Math.Abs(rectangle.Y - targetLocation.Y) * 60 < Math.Abs(velocity.Y))
                {
                    velocity.Y = (targetLocation.Y - rectangle.Y) * 60;
                }

                // Moving customer rectangle
                nonRoundedY += velocity.Y * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
                rectangle.Y = (int)(nonRoundedY + 0.5);
            }
        }

        /// <summary>
        /// Subprogram to set the movement of a Customer
        /// </summary>
        /// <param name="targetLocation">The location the Customer is to move to</param>
        /// <param name="movementTime">The time in which Customer is to move</param>
        public void SetMovement(Vector2 targetLocation)
        {
            // Setting up x component of velocity vector
            if (targetLocation.X - rectangle.X != 0)
            {
                velocity.X = 200 * (targetLocation.X - rectangle.X > 0 ? 1 : -1);
                currentDirection = velocity.X < 0 ? Direction.Left : Direction.Right;
            }
            else
            {
                velocity.X = 0;
            }

            // Setting up y component of velocity vector
            if (targetLocation.Y - rectangle.Y != 0)
            {
                velocity.Y = 200 * (targetLocation.Y - rectangle.Y > 0 ? 1 : -1);
            }
            else
            {
                velocity.Y = 0;
            }

            // Setting various variables to ensure proper correlation of movement
            nonRoundedX = rectangle.X;
            nonRoundedY = rectangle.Y;
            this.targetLocation = targetLocation;
        }
    }
}