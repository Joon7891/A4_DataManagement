// Author: Joon Song
// File Name: CoffeeShopSimulation.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 12/09/2018
// Description: Progarm to simulate a coffee shop

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        // Instances of graphics classes for in-game graphics
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Instance of ContentManager - used for loading various content
        /// </summary>
        public new static ContentManager Content { get; private set; }

        /// <summary>
        /// The current keyboard state
        /// </summary>
        public static KeyboardState NewKeyboard { get; private set; }

        /// <summary>
        /// The state of the keyboard one frame back
        /// </summary>
        public static KeyboardState OldKeyboard { get; private set; }

        // Instance of the coffee shop
        private CoffeeShop coffeeShop = new CoffeeShop();

        private BothCustomer test;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = base.Content;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Setting up window size and mouse as visible
            graphics.PreferredBackBufferHeight = SharedData.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SharedData.SCREEN_WIDTH;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            // Initializing game
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            test = new BothCustomer();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Updating keyboard states
            OldKeyboard = NewKeyboard;
            NewKeyboard = Keyboard.GetState();

            // Updating coffee shop
            coffeeShop.Update(gameTime);

            // Updating game
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Beginning spriteBatch
            spriteBatch.Begin();

            // Drawing coffee shop
            coffeeShop.Draw(spriteBatch);            

            // Ending spriteBatch
            spriteBatch.End();

            // Drawing game
            base.Draw(gameTime);
        }

        /// <summary>
        /// Subprogram to generate a random customer
        /// </summary>
        /// <returns></returns>
        private Customer GenerateRandomCustomer()
        {
            // Caching customer type and customer instance
            int customerType = SharedData.RNG.Next(0, 3);
            Customer customer = null;

            // Constructing appropriate customer given customer type
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

            // Returning randomly generate customer
            return customer;
        }
    }
}
