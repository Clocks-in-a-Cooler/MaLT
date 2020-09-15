using System;
using Microsoft.Xna.Framework;

namespace Tetris {
    public class Board {
        int Width, Height;
        public Vector2 Dimensions {
            get {
                return new Vector2(this.Width, this.Height);
            }
        }
        
        Cell[,] Cells;
        
        public Board(int width, int height) {
            this.Width = width; this.Height = height;
            this.Cells = new Cell[width, height];
            
            for (int x = 0; x < this.Width; x++) {
                for (int y = 0; y < this.Height; y++) {
                    this.Cells[x, y] = new Cell();
                }
            }
        }
        
        public Cell Get_Cell(int x, int y) {
            if (x >= this.Width || x < 0 || y >= this.Height || y < 0) {
                Console.WriteLine("attempted to get a cell outside of this board.");
                return new Cell();
            }
            return this.Cells[x, y];
        }
        
        public void Set_Cell(Cell new_cell, int x, int y) {
            if (x >= this.Width || x < 0 || y >= this.Height || y < 0) {
                Console.WriteLine("attempted to set a cell outside of this board.");
                return; // ignore
            }
            this.Cells[x, y] = new_cell;
        }
        
        public bool Overlaps(Board other, int x_offset, int y_offset) {
            // checks if another board overlaps this one
            // returns true if any non-empty cells on the other board overlap with any non-empty cells with this board
            // or if the other board sticks outside of this one
            if (x_offset < 0 || y_offset < 0 ||
                x_offset + other.Dimensions.X > this.Width ||
                y_offset + other.Dimensions.Y > this.Height
            ) {
                return true;
            }
            
            for (int x = 0; x < other.Dimensions.X; x++) {
                for (int y = 0; y < other.Dimensions.Y; y++) {
                    if (other.Get_Cell(x, y).Is_Empty || this.Get_Cell(x + x_offset, y + y_offset).Is_Empty) {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }
        
        public void Put(Board other, int x_offset, int y_offset, bool set_empties = false) {
            // copies a board's cells to this one
            for (int x = 0; x < other.Dimensions.X; x++) {
                for (int y = 0; y < other.Dimensions.Y; y++) {
                    if (other.Get_Cell(x, y).Is_Empty && !set_empties) {
                        continue;
                    }
                    this.Cells[x + x_offset, y + y_offset] = other.Get_Cell(x, y);
                }
            }
        }

        public Board Rotate(bool counterclockwise = false) {
            // returns a new board rotated by 90 degress
            // by default, it rotates clockwise
            // pass true to this method to make it rotate counterclockwise
            
            // to make it rotate clockwise, keep the y direction the same, but reverse the x direction
            Board new_board = new Board(this.Height, this.Width);
            
            if (counterclockwise) {
                // x and y are relative to the new Board
                for (int x = 0; x < this.Height; x++) {
                    for (int y = 0; y < this.Width; y++) {
                        new_board.Set_Cell(this.Get_Cell(this.Width - 1 - y, x), x, y);
                    }
                }
            } else {
                for (int x = 0; x < this.Height; x++) {
                    for (int y = 0; y < this.Width; y++) {
                        new_board.Set_Cell(this.Get_Cell(y, this.Height - 1 - x), x, y);
                    }
                }
            }
            
            return new_board;
        }
        
        public Board Copy(int x_offset, int y_offset, int width, int height) {
            Board copy = new Board(width, height);
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    copy.Set_Cell(this.Get_Cell(x + x_offset, y + y_offset), x, y);
                }
            }
            return copy;
        }
        
        public bool Check_Row(int row) {
            // returns true if all of the cells in a row are filled
            for (int x = 0; x < this.Width; x++) {
                if (this.Cells[x, row].Is_Empty) {
                    return false;
                }
            }
            
            return true;
        }
        
        public void Clear_Row(int row) {
            // clears all of the cells in a row, moves the rest of the cells down
            for (int x = 0; x < this.Width; x++) {
                this.Cells[x, row].Empty();
            }
            this.Put(this.Copy(0, 0, this.Width, row), 0, 1, true);
        }
    }
}