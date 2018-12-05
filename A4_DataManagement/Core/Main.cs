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

        // Variables related to drawing the background for the leaderboard
        private Texture2D leaderboardBackgroundImage;
        private Rectangle leaderboardBackgroundRectangle;

        // SpriteFont objects to hold various fonts
        private SpriteFont headerFont;
        private SpriteFont informationFont;

        // Instance of the coffee shop
        private CoffeeShop coffeeShop = new CoffeeShop();

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

            // Setting up leaderboard background
            leaderboardBackgroundImage = Content.Load<Texture2D>("Images/Backgrounds/woodBackgroundImage");
            leaderboardBackgroundRectangle = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, 0, SharedData.SCREEN_WIDTH - SharedData.COFFEE_SHOP_WIDTH, SharedData.SCREEN_HEIGHT);

            // Importing various fonts
            headerFont = Content.Load<SpriteFont>("Fonts/HeaderFont");
            informationFont = Content.Load<SpriteFont>("Fonts/InformationFont");
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Beginning spriteBatch
            spriteBatch.Begin();

            // Drawing coffee shop
            coffeeShop.Draw(spriteBatch);

            // Drawing leaderboard background
            spriteBatch.Draw(leaderboardBackgroundImage, leaderboardBackgroundRectangle, Color.White);

            // Drawing leaderboard information
            spriteBatch.DrawString(headerFont, "Statistics", new Vector2(920, 25), Color.ForestGreen);
            spriteBatch.DrawString(informationFont, $"Minimum Wait Time: {(coffeeShop.MinWaitTime != default(double) ? $"{coffeeShop.MinWaitTime}s" : "N/A")}", 
                new Vector2(850, 75), Color.Goldenrod);
            spriteBatch.DrawString(informationFont, $"Maximium Wait Time: {(coffeeShop.MaxWaitTime != default(double) ? $"{coffeeShop.MaxWaitTime}s" : "N/A")}",
                new Vector2(850, 125), Color.Red);
            spriteBatch.DrawString(informationFont, $"Average Wait Time: {(coffeeShop.AverageWaitTime != default(double) ? $"{coffeeShop.AverageWaitTime}s" : "N/A")}", 
                new Vector2(850, 175), Color.White);


            Customer[] customers = coffeeShop.TopFiveCustomersByWaitTime;
            for (int i = 0; i < customers.Length; ++i)
            {
                spriteBatch.DrawString(informationFont, customers[i].Name, new Vector2(850, 50 * i), Color.White);
                spriteBatch.DrawString(informationFont, $"{customers[i].WaitTime}s", new Vector2(950, 50 * i), Color.White);
            }

            //spriteBatch.DrawString(headerFont, "Leaderboard", new Vector2(50, 50), Color.White);

            // Ending spriteBatch
            spriteBatch.End();

            // Drawing game
            base.Draw(gameTime);
        }
    }
}
