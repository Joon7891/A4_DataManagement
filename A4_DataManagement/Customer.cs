// Author: Joon Song
// File Name: Customer.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 12/10/2018
// Description: Class to hold Customer object

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public abstract class Customer
    {
        public string Name { get; }

        public double WaitTime { get => Math.Round(waitTime, 2); }
        private double waitTime = 0;


        private Rectangle rectangle;
        private Texture2D image;

        static Customer()
        {

        }

        public Customer(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Update subprogram for Customer object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            //Incrementing wait time
            waitTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0;
        }

        /// <summary>
        /// Draw subprogram for Customer object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprite</param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}