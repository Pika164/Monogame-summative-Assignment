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
        Screen3,
        Screen4,
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
        Texture2D explosionTexture;
        Texture2D ruinedSidewalkTexture;
        Texture2D ratTexture;
        Texture2D endingScreenTexture;

        Rectangle billyBoxRect;
        Rectangle bombRect;
        Rectangle movingSidewalkRect;
        Rectangle window;
        Rectangle explosionRect;
        Rectangle ratRect;

        Vector2 nukeFalling;
        Vector2 movingSidewalkSpeed;

        Screen screen;

        MouseState mouseState;

        float seconds;

        bool explode;
        bool started;

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

            billyBoxRect = new Rectangle(-50, 250, 250, 200);

            ratRect = new Rectangle(-50, 250, 450, 300);

            bombRect = new Rectangle(150, 0, 250, 200);

            movingSidewalkRect = new Rectangle(0, 0, 1600, 600);

            movingSidewalkSpeed = new Vector2(-1, 0);

            nukeFalling = new Vector2(0, 2);

            explosionRect = new Rectangle(0, 0, 800, 600);

            screen = Screen.Intro;

            seconds = 0f;

            explode = false;

            started = false;

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

            explosionTexture = Content.Load<Texture2D>("explosionCool");

            ruinedSidewalkTexture = Content.Load<Texture2D>("ruinedSidewalk");

            ratTexture = Content.Load<Texture2D>("rat");

            endingScreenTexture = Content.Load<Texture2D>("ending");

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
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (screen != Screen.Intro)
            {
                started = true;
            }

            if (seconds > 14)
            {
                screen = Screen.Screen2;
            }

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

            if (screen == Screen.End)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (window.Contains(mouseState.Position))
                    {
                        Exit();
                    }
                }
            }

            if (screen == Screen.Screen1)
            {
                walkingInstance.Play();
            }

            if (screen == Screen.Screen2)
            {
                walkingInstance.Stop();
                bombRect.Y += (int)nukeFalling.Y;
            }

            if (screen == Screen.Screen3)
            {
                nukeExplosionInstance.Play();
            }

            if (screen == Screen.Screen4)
            {
                nukeExplosionInstance.Stop();
                hiInstance.Play();
            }

            if (screen == Screen.End)
            {
                hiInstance.Stop();
            }

            if (movingSidewalkRect.Right == window.Width)
            {
                movingSidewalkRect.X = 0;
            }

            if (seconds > 18)
            {
                explode = true;
                nukeFalling.Y = 0;
                bombRect.X = 150;
                bombRect.Y = 600;
                screen = Screen.Screen3;
            }

            if (seconds > 22)
            {
                screen = Screen.Screen4;
            }

            if (seconds > 24)
            {
                screen = Screen.End;
            }

            if (seconds > 35)
            {
                Exit();
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
                _spriteBatch.Draw(sidewalkIntroTexture, new Vector2(0, 0), Color.White);

                _spriteBatch.DrawString(startFont, "Click to Start", new Vector2(250, 525), Color.White);
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

            if (screen == Screen.Screen3)
            {
                _spriteBatch.Draw(sidewalkTexture, new Vector2(0, 0), Color.White);

                _spriteBatch.Draw(billyBoxTexture, billyBoxRect, Color.White);

                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);

            }

            if (screen == Screen.Screen4)
            {
                _spriteBatch.Draw(ruinedSidewalkTexture, new Vector2(0,0), Color.White);

                _spriteBatch.Draw(ratTexture, ratRect, Color.White);
            }

            if (screen == Screen.End)
            {
                _spriteBatch.Draw(endingScreenTexture, new Vector2(0,0), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
