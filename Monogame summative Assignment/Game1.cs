using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_summative_Assignment
{

    enum Screen
    {
        Intro,
        Screen1,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D billyBoxTexture;
        Texture2D bombTexture;
        Texture2D sidewalkTexture;

        Rectangle billyBoxRect;
        Rectangle bombRect;
        Rectangle window;

        Screen screen;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            billyBoxRect = new Rectangle(0,0,100,100);

            screen = Screen.Intro;


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            billyBoxTexture = Content.Load<Texture2D>("billyBox");

            bombTexture = Content.Load<Texture2D>("bombYay");

            sidewalkTexture = Content.Load<Texture2D>("sidewalk");
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Screen1)
            {
                _spriteBatch.Draw(billyBoxTexture, billyBoxRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
