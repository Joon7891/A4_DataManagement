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

        /// <summary>
        /// The current mouse state
        /// </summary>
        public static MouseState NewMouse { get; private set; }

        /// <summary>
        /// The state of the mouse one frame back
        /// </summary>
        public static MouseState OldMouse { get; private set; }

        // Instance of the coffee shop
        private CoffeeShop coffeeShop = new CoffeeShop();
        private Customer[] topFiveCustomers = new Customer[0];

        // Time and status related variables
        private Vector2 timeLeftLoc = new Vector2(HORIZONTAL_TEXT_BUFFER, 550);
        private double timeLeft = 300;
        private Button[] statusButtons = new Button[2];

        // Variables related to drawing the background for the leaderboard
        private const int BORDER_SIZE = 6;
        private Texture2D borderImage;
        private Rectangle[] borderRectangles = new Rectangle[7];
        private Texture2D leaderboardBackgroundImage;
        private Rectangle leaderboardBackgroundRectangle;

        // Variables required to draw various leaderboard data
        private const int HORIZONTAL_TEXT_BUFFER = 820;
        private const int HORIZONTAL_INFO_BUFFER = 1100;
        private Vector2[] headerTextLocs = new Vector2[9];
        private Vector2[] waitTimeLocs = new Vector2[11];

        // SpriteFont objects to hold various fonts
        private SpriteFont headerFont;
        private SpriteFont subHeaderFont;
        private SpriteFont informationFont;
        
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
            borderImage = Content.Load<Texture2D>("Images/Sprites/whiteImage");
            borderRectangles[0] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, 0, BORDER_SIZE, SharedData.SCREEN_HEIGHT);
            borderRectangles[1] = new Rectangle(SharedData.SCREEN_WIDTH - BORDER_SIZE, 0, BORDER_SIZE, SharedData.SCREEN_HEIGHT);
            borderRectangles[2] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, 0, SharedData.SCREEN_WIDTH - SharedData.CUSTOMER_WIDTH, BORDER_SIZE);
            borderRectangles[3] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, BORDER_SIZE + 60, SharedData.SCREEN_WIDTH - SharedData.CUSTOMER_WIDTH, BORDER_SIZE / 2);
            borderRectangles[4] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, BORDER_SIZE + 250, SharedData.SCREEN_WIDTH - SharedData.CUSTOMER_WIDTH, BORDER_SIZE / 2);
            borderRectangles[5] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, BORDER_SIZE + 520, SharedData.SCREEN_WIDTH - SharedData.CUSTOMER_WIDTH, BORDER_SIZE / 2);
            borderRectangles[6] = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, SharedData.SCREEN_HEIGHT - BORDER_SIZE, SharedData.SCREEN_WIDTH - SharedData.CUSTOMER_WIDTH, BORDER_SIZE);
            leaderboardBackgroundImage = Content.Load<Texture2D>("Images/Backgrounds/woodBackgroundImage");
            leaderboardBackgroundRectangle = new Rectangle(SharedData.COFFEE_SHOP_WIDTH, 0, SharedData.SCREEN_WIDTH - SharedData.COFFEE_SHOP_WIDTH, SharedData.SCREEN_HEIGHT);

            // Setting up statistics text locations
            headerTextLocs[0] = new Vector2(920, 20);
            for (int i = 0; i < headerTextLocs.Length / 2; ++i)
            {
                headerTextLocs[2 * i + 1] = new Vector2(HORIZONTAL_TEXT_BUFFER, 89 + 40 * i);
                headerTextLocs[2 * i + 2] = new Vector2(HORIZONTAL_INFO_BUFFER, 89 + 40 * i);
            }
            waitTimeLocs[0] = new Vector2(868, 279);
            for (int i = 0; i < waitTimeLocs.Length / 2; ++i)
            {
                waitTimeLocs[2 * i + 1] = new Vector2(HORIZONTAL_TEXT_BUFFER, 320 + 40 * i);
                waitTimeLocs[2 * i + 2] = new Vector2(HORIZONTAL_INFO_BUFFER, 320 + 40 * i);
            }

            // Importing various fonts
            headerFont = Content.Load<SpriteFont>("Fonts/HeaderFont");
            subHeaderFont = Content.Load<SpriteFont>("Fonts/SubHeaderFont");
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
            // Updating keyboard and mouse states
            OldKeyboard = NewKeyboard;
            OldMouse = NewMouse;
            NewKeyboard = Keyboard.GetState();
            NewMouse = Mouse.GetState();

            // Updating time left
            timeLeft -= gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // Updating coffee shop and caching top five customers if simluation is active
            if (true)
            {
                coffeeShop.Update(gameTime);
                topFiveCustomers = coffeeShop.TopFiveCustomersByWaitTime;
            }

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

            // Drawing leaderboard background with borders
            spriteBatch.Draw(leaderboardBackgroundImage, leaderboardBackgroundRectangle, Color.White);
            for (int i = 0; i < borderRectangles.Length; ++i)
            {
                spriteBatch.Draw(borderImage, borderRectangles[i], Color.White);
            }

            // Drawing max, min, and average wait time information
            spriteBatch.DrawString(headerFont, "Statistics", headerTextLocs[0], Color.ForestGreen);
            spriteBatch.DrawString(informationFont, $"Total Served:", headerTextLocs[1], Color.White);
            spriteBatch.DrawString(informationFont, $"{coffeeShop.TotalServed}", headerTextLocs[2], Color.White);
            spriteBatch.DrawString(informationFont, $"Average Wait Time:", headerTextLocs[3], Color.LightCyan);
            spriteBatch.DrawString(informationFont, $"{coffeeShop.AverageWaitTime}s", headerTextLocs[4], Color.LightCyan);
            spriteBatch.DrawString(informationFont, $"Minimum Wait Time:", headerTextLocs[5], Color.Goldenrod);
            spriteBatch.DrawString(informationFont, $"{coffeeShop.MinWaitTime}s", headerTextLocs[6], Color.Goldenrod);
            spriteBatch.DrawString(informationFont, $"Maximum Wait Time:", headerTextLocs[7], Color.Red);
            spriteBatch.DrawString(informationFont, $"{coffeeShop.MaxWaitTime}s", headerTextLocs[8], Color.Red);

            // Drawing top five customers by wait time information
            spriteBatch.DrawString(subHeaderFont, "Top Five Customers", waitTimeLocs[0], Color.White);
            for (int i = 0; i < 5; ++i)
            {
                spriteBatch.DrawString(informationFont, $"{i + 1}) {(i < topFiveCustomers.Length ? topFiveCustomers[i].Name : "N/A")}", waitTimeLocs[2 * i + 1], Color.White);
                spriteBatch.DrawString(informationFont, $"{(i < topFiveCustomers.Length ? topFiveCustomers[i].WaitTime : 0.0)}s", waitTimeLocs[2 * i + 2], Color.White);
            }

            // Drawing time remaining and buttons
            spriteBatch.DrawString(informationFont, $"Time Left: {Math.Round(timeLeft, 2)}s", timeLeftLoc, Color.White);

            // Ending spriteBatch
            spriteBatch.End();

            // Drawing game
            base.Draw(gameTime);
        }
    }
}