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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MainMenu mainMenu;
        private Board board;
        private Input input;
        private Color PuzzleKingsYellow = new Color(254, 226, 47);

        public enum EGameStates { mainmenu, ingame, options };
        private EGameStates eGameState = EGameStates.mainmenu;

        private bool gameStarted;

        private int screenWidth;
        private int screenHeight;

        public EGameStates EGameState
        {
            get
            {
                return eGameState;
            }
            set
            {
                eGameState = value;
            }
        }


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

            mainMenu    = new MainMenu(Content, screenWidth, screenHeight);
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
            switch (eGameState)
            {
                case EGameStates.mainmenu:
                    input.Update(this, mainMenu);
                    mainMenu.Update();
                    break;
                case EGameStates.ingame:
                    //Checks for device input every tick
                    if (board.TileMoving == true)
                    {
                        board.TileArray[board.ToMove.Num].MovingPos(board.ToMove, board.MovingTo, board);
                    }
                    else
                    {
                        input.Update(this, board);
                    }
                    break;
                case EGameStates.options:
                    input.Update(this, mainMenu);
                    break;
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
            GraphicsDevice.Clear(PuzzleKingsYellow);

            switch (eGameState)
            {
                case EGameStates.mainmenu:
                    mainMenu.Draw(spriteBatch);
                    break;
                case EGameStates.ingame:
                    //Draws the game board
                    board.DrawBoard();
                    break;
                case EGameStates.options:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
