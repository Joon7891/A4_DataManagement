// Author: Joon Song
// File Name: CoffeeShop.cs
// Project Name: A4_DataManagement
// Creation Date: 11/28/2018
// Modified Date: 11/28/2018

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
    public sealed class CoffeeShop
    {
        private CashierLine cashierLine;
        private LineupQueue customerLine;

        /// <summary>
        /// Update subprogram for CoffeeShop object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating both lines
            cashierLine.Update(gameTime);
        }

        /// <summary>
        /// Draw subprogram for CoffeeShop object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing both lines
            cashierLine.Draw(spriteBatch);
        }
    }
}
