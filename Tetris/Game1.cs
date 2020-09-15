using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _sprite_batch;
        private static int _cell_width = 25;
        
        private Keytracker[] _keytrackers;
        
        private Board _tetris_board;
        private Board _current_piece;
        private Vector2 _piece_position;
        private int _since_last_drop;

        public Game1() {
            _graphics             = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible        = true;
            _keytrackers          = new Keytracker[4];
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            
            _graphics.PreferredBackBufferWidth  = 500;
            _graphics.PreferredBackBufferHeight = 600;
            
            _keytrackers[0] = new Keytracker(Keys.Up, () => {
                if (_current_piece != null) {
                    // first generate a new position and piece.
                    // only rotate if the new piece in the new position
                    // doesn't overlap with the board or stick out of it
                    Board new_piece      = _current_piece.Rotate();
                    Vector2 new_position = new Vector2(_piece_position.X, _piece_position.Y);
                    
                    new_position.X += (int) ((_current_piece.Dimensions.Y - _current_piece.Dimensions.X) / 2);
                    new_position.Y += (int) ((_current_piece.Dimensions.X - _current_piece.Dimensions.Y) / 2);
                    
                    if (!_tetris_board.Overlaps(new_piece, (int) new_position.X, (int) new_position.Y)) {
                        _current_piece  = new_piece;
                        _piece_position = new_position;
                    }
                }
            });
            
            _keytrackers[1] = new Keytracker(Keys.Down, () => {
                if (_current_piece != null) {
                    // _piece_position.Y += 1;
                }
            });
            
            _keytrackers[2] = new Keytracker(Keys.Left, () => {
                if (_current_piece != null &&
                    !_tetris_board.Overlaps(_current_piece, (int) (_piece_position.X - 1), (int) _piece_position.Y)
                ) {
                    _piece_position.X -= 1;
                }
            });
            
            _keytrackers[3] = new Keytracker(Keys.Right, () => {
                if (_current_piece != null &&
                    !_tetris_board.Overlaps(_current_piece, (int) (_piece_position.X + 1), (int) _piece_position.Y)
                ) {
                    _piece_position.X += 1;
                }
            });
            
            _tetris_board    = new Board(14, 20);
            _since_last_drop = 0;
            
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent() {
            _sprite_batch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            KeyboardState keys = Keyboard.GetState();
            
            foreach (Keytracker k in _keytrackers) {
                k.Update(keys);
            }
            
            if (_current_piece == null) {
                _current_piece = Pieces.Choose_Random();
                // place it at the middle of the top row
                _piece_position = new Vector2(
                    (int) ((_tetris_board.Dimensions.X - _current_piece.Dimensions.X) / 2), 1
                );
                
                base.Update(gameTime);
                return;
            }
            
            _since_last_drop += (int) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_since_last_drop >= 600) {
                if (!_tetris_board.Overlaps(_current_piece, (int) _piece_position.X, (int) (_piece_position.Y + 1))) {
                    _since_last_drop   = 0;
                    _piece_position.Y += 1;
                } else {
                    _tetris_board.Put(_current_piece, (int) _piece_position.X, (int) _piece_position.Y);
                    _current_piece = null;
                    
                    // clear some rows!
                    for (int y = 0; y < _tetris_board.Dimensions.Y; y++) {
                        if (_tetris_board.Check_Row(y)) {
                            _tetris_board.Clear_Row(y);
                        }
                    }
                }
            }
            
            /*
            if (_piece_position.Y + _current_piece.Dimensions.Y >= _tetris_board.Dimensions.Y) {
                _current_piece = null;
            } */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.PowderBlue);

            // TODO: Add your drawing code here
            
            for (int x = 0; x < _tetris_board.Dimensions.X; x++) {
                for (int y = 0; y < _tetris_board.Dimensions.Y; y++) {
                    Draw_Rectangle(new Vector2(x * 25 + 25, y * 25 + 50), new Vector2(25, 25), _tetris_board.Get_Cell(x, y).Colour);
                }
            }
            
            if (_current_piece != null) {
                for (int x = 0; x < _current_piece.Dimensions.X; x++) {
                    for (int y = 0; y < _current_piece.Dimensions.Y; y++) {
                        if (_current_piece.Get_Cell(x, y).Is_Empty) {
                            continue;
                        }
                        Draw_Rectangle(new Vector2((x + _piece_position.X) * 25 + 25, (y + _piece_position.Y) * 25 + 50), new Vector2(25, 25), _current_piece.Get_Cell(x, y).Colour);
                    }
                }
            }

            base.Draw(gameTime);
        }
        
        void Draw_Rectangle(Vector2 position, Vector2 size, Color colour) {
            // thanks StackOverflow! (nvoigt and Zillo)
            Color[] data = new Color[(int) size.X * (int) size.Y];
            Texture2D texture = new Texture2D(_graphics.GraphicsDevice, (int) size.X, (int) size.Y);
            
            for (int c = 0; c < data.Length; c++) {
                data[c] = colour;
            }
            
            texture.SetData(data);
            
            _sprite_batch.Begin();
            _sprite_batch.Draw(texture, position, colour);
            _sprite_batch.End();
        }
        
        void Draw_Rectangle(int x, int y, int width, int height, Color colour) {
            Draw_Rectangle(new Vector2(x, y), new Vector2(width, height), colour);
        }
    }
}
