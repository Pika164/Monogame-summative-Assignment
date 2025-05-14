using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_summative_Assignment
{

    enum Screen
    {
        Intro,
        Screen1,
        Screen2,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont startFont;

        Texture2D billyBoxTexture;
        Texture2D bombTexture;
        Texture2D sidewalkTexture;
        Texture2D sidewalkIntroTexture;
        Texture2D movingSidewalkTexture;

        Rectangle billyBoxRect;
        Rectangle bombRect;
        Rectangle movingSidewalkRect;
        Rectangle window;

        Vector2 nukeFalling;
        Vector2 movingSidewalkSpeed;

        Screen screen;

        MouseState mouseState;

        float seconds;

        SoundEffect walkingSound;
        SoundEffectInstance walkingInstance;

        SoundEffect nukeExplosion;
        SoundEffectInstance nukeExplosionInstance;

        SoundEffect hiSound;
        SoundEffectInstance hiInstance;


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

            billyBoxRect = new Rectangle(-50,250,250,200);

            bombRect = new Rectangle(150,-15,250,200);

            movingSidewalkRect = new Rectangle(0,0,1600,600);

            movingSidewalkSpeed = new Vector2(-1,0);

            nukeFalling = new Vector2(0,5);

            screen = Screen.Intro;

            seconds = 0f;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            billyBoxTexture = Content.Load<Texture2D>("billyBox");

            bombTexture = Content.Load<Texture2D>("bombYay");

            sidewalkTexture = Content.Load<Texture2D>("sidewalk");

            sidewalkIntroTexture = Content.Load<Texture2D>("sidewalkIntro");

            movingSidewalkTexture = Content.Load<Texture2D>("movingSidewalk");

            startFont = Content.Load<SpriteFont>("spriteFont");

            walkingSound = Content.Load<SoundEffect>("walkingSounds");

            walkingInstance = walkingSound.CreateInstance();

            nukeExplosion = Content.Load<SoundEffect>("nukeExplosion");

            nukeExplosionInstance = nukeExplosion.CreateInstance();

            hiSound = Content.Load<SoundEffect>("hisound");

            hiInstance = hiSound.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();

            movingSidewalkRect.X += (int)movingSidewalkSpeed.X;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Screen1;
                }
            }

            if (screen == Screen.Screen1)
            {
                walkingInstance.Play();
            }

            if (walkingInstance.State == SoundState.Stopped && screen == Screen.Screen1)
            {
                screen = Screen.Screen2;
            }

            if (movingSidewalkRect.Right == window.Width)
            {
                movingSidewalkRect.X = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(sidewalkIntroTexture, new Vector2(0,0), Color.White);

                _spriteBatch.DrawString(startFont, "Click to Start", new Vector2(250,525), Color.White);
            }

            if (screen == Screen.Screen1)
            {

                _spriteBatch.Draw(movingSidewalkTexture, movingSidewalkRect, Color.White);

                _spriteBatch.Draw(billyBoxTexture, billyBoxRect, Color.White);
            }

            if (screen == Screen.Screen2)
            {
                _spriteBatch.Draw(sidewalkTexture, new Vector2(0, 0), Color.White);

                _spriteBatch.Draw(billyBoxTexture, billyBoxRect, Color.White);

                _spriteBatch.Draw(bombTexture, bombRect, Color.White);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
