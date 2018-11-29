// Author: Joon Song
// File Name: IEntity.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Interface to contain definitions for an entity

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
    public interface IEntity
    {
        /// <summary>
        /// Update subprogram for an entity object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw subprogram for an entity objecty
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
