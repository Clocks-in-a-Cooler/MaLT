using System;
using Microsoft.Xna.Framework;

namespace Tetris {
    public static class Pieces {
        // all those tetriminos!
        public static Board Square {
            get {
                Board square = new Board(2, 2);
                square.Set_Cell(new Cell(Color.Gold), 0, 0);
                square.Set_Cell(new Cell(Color.Gold), 1, 0);
                square.Set_Cell(new Cell(Color.Gold), 0, 1);
                square.Set_Cell(new Cell(Color.Gold), 1, 1);
                
                return square;
            }
        }
        
        public static Board Line {
            get {
                Board line = new Board(4, 1);
                line.Set_Cell(new Cell(Color.DarkTurquoise), 0, 0);
                line.Set_Cell(new Cell(Color.DarkTurquoise), 1, 0);
                line.Set_Cell(new Cell(Color.DarkTurquoise), 2, 0);
                line.Set_Cell(new Cell(Color.DarkTurquoise), 3, 0);
                
                return line;
            }
        }
        
        public static Board L {
            get {
                Board l = new Board(3, 2);
                l.Set_Cell(new Cell(Color.Orange), 2, 0);
                l.Set_Cell(new Cell(Color.Orange), 0, 1);
                l.Set_Cell(new Cell(Color.Orange), 1, 1);
                l.Set_Cell(new Cell(Color.Orange), 2, 1);
                
                return l;
            }
        }
        
        public static Board J {
            get {
                Board j = new Board(3, 2);
                j.Set_Cell(new Cell(Color.RoyalBlue), 0, 0);
                j.Set_Cell(new Cell(Color.RoyalBlue), 0, 1);
                j.Set_Cell(new Cell(Color.RoyalBlue), 1, 1);
                j.Set_Cell(new Cell(Color.RoyalBlue), 2, 1);
                
                return j;
            }
        }
        
        public static Board T {
            get {
                Board t = new Board(3, 2);
                t.Set_Cell(new Cell(Color.DarkViolet), 1, 0);
                t.Set_Cell(new Cell(Color.DarkViolet), 0, 1);
                t.Set_Cell(new Cell(Color.DarkViolet), 1, 1);
                t.Set_Cell(new Cell(Color.DarkViolet), 2, 1);
                
                return t;
            }
        }
        
        public static Board S {
            get {
                Board s = new Board(3, 2);
                s.Set_Cell(new Cell(Color.LawnGreen), 1, 0);
                s.Set_Cell(new Cell(Color.LawnGreen), 2, 0);
                s.Set_Cell(new Cell(Color.LawnGreen), 0, 1);
                s.Set_Cell(new Cell(Color.LawnGreen), 1, 1);
                
                return s;
            }
        }
        
        public static Board Z {
            get {
                Board z = new Board(3, 2);
                z.Set_Cell(new Cell(Color.Crimson), 0, 0);
                z.Set_Cell(new Cell(Color.Crimson), 1, 0);
                z.Set_Cell(new Cell(Color.Crimson), 1, 1);
                z.Set_Cell(new Cell(Color.Crimson), 2, 1);
                
                return z;
            }
        }
        
        private static Board[] _pieces = new Board[7] { Square, Line, L, J, T, S, Z };
        
        public static Board Choose_Random() {
            Random rnd = new Random();
            return _pieces[rnd.Next(0, 7)];
        }
    }
}