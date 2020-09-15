using System;
using Microsoft.Xna.Framework.Input;

// tracks a key, invokes a delegate when the key is pressed (but not held)

namespace Tetris {
    public delegate void On_Keystroke();
    
    public class Keytracker {
        public On_Keystroke _keystroke;
        
        Keys _tracking_key;
        
        bool _pressed;
        public bool Is_Pressed {
            get {
                return _pressed;
            }
        }
        
        public Keytracker(Keys key, On_Keystroke keystroke) {
            this._tracking_key = key;
            this._keystroke    = keystroke;
            this._pressed      = false;
        }
        
        public void Update(KeyboardState keys) {
            if (keys.IsKeyDown(this._tracking_key) && !this._pressed) {
                this._pressed = true;
                this._keystroke();
            }
            
            this._pressed = this._pressed && keys.IsKeyDown(this._tracking_key);
        }
    }
}