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
    public class CoffeeShopSimulation : Game
    {
        // Instances of graphics classes for in-game graphics
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Instance of ContentManager - used for loading various content
        /// </summary>
        public new ContentManager Content { get; private set; }

        /// <summary>
        /// The current keyboard state
        /// </summary>
        public static KeyboardState NewKeyboard { get; private set; }

        /// <summary>
        /// The state of the keyboard one frame back
        /// </summary>
        public static KeyboardState OldKeyboard { get; private set; }

        // Customer related-data
        private ServiceLine cashierCustomers = new ServiceLine();
        private LineupQueue lineupCustomers = new LineupQueue();
        private const int ADD_TIME = 3;
        private float addTimer = 0;

        public CoffeeShopSimulation()
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

            // Updating customers in line up and cashier
            lineupCustomers.Update(gameTime);
            cashierCustomers.Update(gameTime);

            Console.WriteLine($"{cashierCustomers.TotalCount} - {4 - cashierCustomers.SpotsAvailable} - {cashierCustomers.SpotsAvailable} - {lineupCustomers.InsideCustomersCount} - {lineupCustomers.Count}");

            // Adding a customer to the service line every 3 seconds
            addTimer += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (addTimer >= ADD_TIME)
            {
                lineupCustomers.Enqueue(GenerateRandomCustomer());
                addTimer -= ADD_TIME;
            }
            
            // Moving customer from lineup to cashier if possible
            if (cashierCustomers.SpotsAvailable > 0 && lineupCustomers.Count > 0)
            {
                cashierCustomers.Add(lineupCustomers.Dequeue());
            }

            // Moving a customer from the outside of the shop to the inside, if possible
            if (cashierCustomers.TotalCount + lineupCustomers.InsideCustomersCount < 20)
            {
                lineupCustomers.MoveInside();
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
