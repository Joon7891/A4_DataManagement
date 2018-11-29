// Author: Joon Song
// File Name: CoffeeShop.cs
// Project Name: A4_DataManagement
// Creation Date: 11/28/2018
// Modified Date: 11/28/2018
// Description: Class to hold CoffeeShop object; inherits from IEntity interface

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
    public sealed class CoffeeShop : IEntity
    {
        private CashierLine cashierLine = new CashierLine();
        private InsideLineQueue insideLine = new InsideLineQueue();
        private OutsideLineQueue outsideLine = new OutsideLineQueue();

        /// <summary>
        /// Update subprogram for CoffeeShop object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating all lines
            cashierLine.Update(gameTime);
            insideLine.Update(gameTime);
            outsideLine.Update(gameTime);
        }

        /// <summary>
        /// Draw subprogram for CoffeeShop object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing all lines
            cashierLine.Draw(spriteBatch);
            insideLine.Draw(spriteBatch);
            outsideLine.Draw(spriteBatch);
        }
    }
}
