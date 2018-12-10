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
        public bool IsMoving => targetQueue.Size > 0 || !(rectangle.X == currentTarget.X && rectangle.Y == currentTarget.Y);

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
        private ListQueue<Vector2> targetQueue = new ListQueue<Vector2>();
        private Rectangle rectangle;
        private Vector2 currentTarget;
        private Vector2 velocity;
        private double nonRoundedX;
        private double nonRoundedY;

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
            rectangle = new Rectangle(SharedData.COFFEE_SHOP_WIDTH + SharedData.CUSTOMER_WIDTH, SharedData.VERTICAL_BUFFER + 6 * SharedData.CUSTOMER_HEIGHT,
                SharedData.CUSTOMER_WIDTH, SharedData.CUSTOMER_HEIGHT);
            currentTarget.X = rectangle.X;
            currentTarget.Y = rectangle.Y;
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

            // Moving onto next target if Customer has reached current target
            if (rectangle.X == currentTarget.X && rectangle.Y == currentTarget.Y && targetQueue.Size > 0)
            {
                currentTarget = targetQueue.Dequeue();
                SetMovement();
            }

            // Moving customer in appropriate direction
            if (currentDirection == Direction.Left || currentDirection == Direction.Right)
            {
                // Adjusting x-velocity if horizontal movement will overshoot
                if (Math.Abs(nonRoundedX - currentTarget.X) * 1000 / gameTime.ElapsedGameTime.Milliseconds < Math.Abs(velocity.X))
                {
                    velocity.X = (currentTarget.X - rectangle.X) * 1000 / gameTime.ElapsedGameTime.Milliseconds;
                }

                // Moving customer rectangle
                nonRoundedX += velocity.X * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
                rectangle.X = (int)(nonRoundedX + 0.5);

                // Changing direction to vertical if customer should now move vertically
                if (rectangle.X == currentTarget.X && rectangle.Y != currentTarget.Y)
                {
                    currentDirection = velocity.Y > 0 ? Direction.Down : Direction.Up;
                }
            }
            else
            {
                // Adjusting y-velocity if vertical movement will overshoot
                if (Math.Abs(nonRoundedY - currentTarget.Y) * 1000 / gameTime.ElapsedGameTime.Milliseconds < Math.Abs(velocity.Y))
                {
                    velocity.Y = (currentTarget.Y - rectangle.Y) * 1000 / gameTime.ElapsedGameTime.Milliseconds;
                }

                // Moving customer rectangle
                nonRoundedY += velocity.Y * gameTime.ElapsedGameTime.Milliseconds / 1000.0;
                rectangle.Y = (int)(nonRoundedY + 0.5);

                // Changing direction to vertical if customer should now move horizontally
                if (rectangle.Y == currentTarget.Y && rectangle.X != currentTarget.X)
                {
                    currentDirection = velocity.X > 0 ? Direction.Right : Direction.Left;
                }
            }

            // Setting customer direction to up if customer has finished moving
            if (!IsMoving)
            {
                currentDirection = Direction.Up;
            }
        }

        /// <summary>
        /// Subprogram to add a number of target locations for the customer 
        /// </summary>
        /// <param name="targetLocations">An array of target locations</param>
        public void AddTargetLocations(params Vector2[] targetLocations)
        {
            // Adding target locations to target queue
            for (int i = 0; i < targetLocations.Length; ++i)
            {
                targetQueue.Enqueue(targetLocations[i]);
            }
        }

        /// <summary>
        /// Subprogram to set the movement of the Customer to the current target
        /// </summary>
        private void SetMovement()
        {
            // Setting up velocity vector
            velocity.X = (currentTarget.X - rectangle.X == 0) ? 0 : 200 * (currentTarget.X - rectangle.X > 0 ? 1 : -1);
            velocity.Y = (currentTarget.Y - rectangle.Y == 0) ? 0 : 200 * (currentTarget.Y - rectangle.Y > 0 ? 1 : -1);

            // Setting up current direction
            currentDirection = velocity.X < 0 ? Direction.Left : Direction.Right;

            // Setting various variables to ensure proper correlation of movement
            nonRoundedX = rectangle.X;
            nonRoundedY = rectangle.Y;
       }
    }
}