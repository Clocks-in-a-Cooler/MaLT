using System;
using Microsoft.Xna.Framework;

namespace Tetris {
    public class Cell {
        public static Color Default_Colour = new Color(37, 0, 65);
        private Color _colour;
        public Color Colour {
            get {
                if (this.Is_Empty) {
                    return Default_Colour;
                }
                return _colour;
            }
            set {
                _colour = value;
            }
        }
        
        public bool Is_Empty;
        
        public Cell() {
            this.Is_Empty = true;
        }
        
        public Cell(Color colour) {
            this.Colour   = colour;
            this.Is_Empty = false;
        }
        
        public void Fill(Color new_colour) {
            this.Is_Empty = false;
            this.Colour   = new_colour;
        }
        
        public void Empty() {
            this.Is_Empty = true;
        }
    }
}