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

        // Time-related variables
        private const int ADD_TIME = 3;
        private float addTimer = 0;

        /// <summary>
        /// Update subprogram for CoffeeShop object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Callling adding a random customer every 3 seconds
            addTimer += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (addTimer >= ADD_TIME)
            {
                outsideLine.Enqueue(RandomCustomer());
                addTimer -= ADD_TIME;
            }

            // Updating all lines
            cashierLine.Update(gameTime);
            insideLine.Update(gameTime);
            outsideLine.Update(gameTime);

            // Moving outside customer inside if possible
            if (outsideLine.Size > 0 && cashierLine.Count + insideLine.Size < 20)
            {
                insideLine.Enqueue(outsideLine.Dequeue());
            }

            // Moving line customer to cashier line if possible
            if (insideLine.Size > 0 && cashierLine.CashiersAvailable > 0)
            {
                cashierLine.AddCustomer(insideLine.Dequeue());
            }
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

        /// <summary>
        /// Subprogram to generate a random customer
        /// </summary>
        /// <returns>The random customer</returns>
        public Customer RandomCustomer()
        {
            // Variables related to random customer generation
            int customerType = SharedData.RNG.Next(0, 3);
            Customer customer = null;

            // Constructing appropraite customer type
            switch(customerType)
            {
                case 0:
                    customer = new CoffeeCustomer();
                    break;

                case 1:
                    customer = new FoodCustomer();
                    break;

                case 2:
                    customer = new BothCustomer();
                    break;
            }

            // Returning randomly generated customer
            return customer;
        }
    }
}
