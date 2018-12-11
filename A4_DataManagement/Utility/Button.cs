// Author: Joon Song
//Project Name: A4_DataManagement
// File Name: Button.cs
// Creation Date: 09/10/2018
// Modified Date: 09/18/2018
// Desription: Class to hold Button, an object that represents an on-screen button, implements the entity interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace A4_DataManagement
{
    public sealed class Button : IEntity
    {
        // Variables button click related fields
        private static SoundEffect clickSoundEffect;
        public delegate void OnClick();
        private OnClick onClick;

        // Variables to hold the image and rectangular dimensions of button
        private Texture2D image;
        private Rectangle rect;

        /// <summary>
        /// Whether the mouse is hovering above the button
        /// </summary>
        public bool IsMouseHovering => CollisionHelper.PointToRect(Main.NewMouse.Position.ToVector2(), rect);

        /// <summary>
        /// Static constructor to setup Button components
        /// </summary>
        static Button()
        {
            clickSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/buttonClick");
        }

        /// <summary>
        /// Constructor for button object
        /// </summary>
        /// <param name="image">The image of the button</param>
        /// <param name="rect">The rectangle representing the rectangular dimensions of the button</param>
        /// <param name="onClick">The behavior of the button when clicked</param>
        public Button(Texture2D image, Rectangle rect, OnClick onClick)
        {
            // Setting up button variables from parameters
            this.onClick = onClick;
            this.image = image;
            this.rect = rect;
        }

        /// <summary>
        /// Update various button components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Invoking button behavior and sound if button is clicked
            if (MouseHelper.NewClick() && IsMouseHovering)
            {
                clickSoundEffect.CreateInstance().Play();
                onClick();
            }
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing button; transparency is higher if mouse is not hovering over button
            spriteBatch.Draw(image, rect, Color.White * (0.6f + 0.4f * Convert.ToByte(IsMouseHovering)));
        }
    }
}