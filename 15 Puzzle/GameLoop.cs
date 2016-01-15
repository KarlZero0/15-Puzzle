using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _15_Puzzle
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameLoop : Game
    {
        //Declares starting variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Board board;
        Input input;
        bool gameStarted;

        int screenWidth;
        int screenHeight;

        //Constructor for main game loop
        public GameLoop()
        {
            //Instantiates classes and sets variables
            graphics              = new GraphicsDeviceManager(this);
            input                 = new Input();
            gameStarted           = false;
            Content.RootDirectory = "Content";
            //Makes the mouse visible
            this.IsMouseVisible   = true;
            
        }

        //Creates property to check if the game is in progress
        public bool GameStarted
        {
            get
            {
                return gameStarted;
            }
            set
            {
                gameStarted = value;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Sets window size and fullscreen
            graphics.PreferredBackBufferWidth  = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen              = true;
            graphics.ApplyChanges();
            
            //Stores window size
            screenHeight                       = GraphicsDevice.PresentationParameters.BackBufferHeight;
            screenWidth                        = GraphicsDevice.PresentationParameters.BackBufferWidth;

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

            // Instantiates class using window size
            board       = new Board(Content, spriteBatch, screenWidth, screenHeight);
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
            //Checks for device input every tick
            if (board.TileMoving == true)
            {
                board.TileArray[board.ToMove.Num].MovingPos(board.ToMove, board.MovingTo, board);
            }
            else
            {
                input.Update(this, board);
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            //Draws the game board
            board.DrawBoard();

            base.Draw(gameTime);
        }
    }
}
