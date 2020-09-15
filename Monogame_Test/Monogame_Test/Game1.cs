using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
    next steps:
    https://docs.monogame.net/articles/getting_started/5_adding_basic_code.html
    Monodevelop isn't too bad...   
 */

namespace Monogame_Test {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D Ball_Texture;

        Vector2 Ball_Position;
        float Ball_Speed;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            /*
             * This method is called after the constructor,
             * but before the main game loop(Update/Draw).
             * This is where you can query any required services
             * and load any non-graphic related content.
             */

            // put the ball in the center of the screen
            Ball_Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            Ball_Speed = 100f;

            // resize the window
            _graphics.PreferredBackBufferWidth  = 400;
            _graphics.PreferredBackBufferHeight = 600;

            _graphics.ApplyChanges();

            base.Initialize();

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /*
                This method is used to load your game content.
                It is called only once per game, after Initialize method,
                but before the main game loop methods.
            */

            // load the ball
            Ball_Texture = Content.Load<Texture2D>("ball");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*
                This method is called multiple times per second,
                and is used to update your game state (checking for collisions,
                gathering input, playing audio, etc.).
            */

            // movement
            KeyboardState keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.Up) || keyboard_state.IsKeyDown(Keys.W)) {
                Ball_Position.Y -= Ball_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Down) || keyboard_state.IsKeyDown(Keys.S)) {
                Ball_Position.Y += Ball_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Left) || keyboard_state.IsKeyDown(Keys.A)) {
                Ball_Position.X -= Ball_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Right) || keyboard_state.IsKeyDown(Keys.D)) {
                Ball_Position.X += Ball_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DodgerBlue);

            // TODO: Add your drawing code here

            // draw the ball
            _spriteBatch.Begin();

            //_spriteBatch.Draw(Ball_Texture, Ball_Position, Color.White); // i protest. it should be "Colour"
            // the above draws the top left corner of the ball at the center. to center the ball, use the line below
            _spriteBatch.Draw(Ball_Texture, Ball_Position, null, Color.White, 0f, new Vector2(Ball_Texture.Width / 2, Ball_Texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
