using System;

public struct Vector {
    public int X, Y;
    public Vector(int x, int y) {
        this.X = x; this.Y = y;
    }
    
    // this is one thing i can't do in Javascript
    public static Vector operator +(Vector a, Vector b) {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }
    
    public static Vector operator -(Vector a, Vector b) {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }
    
    public static Vector operator *(Vector a, double factor) {
        return new Vector((int)(a.X * factor), (int)(a.Y * factor));
    }
    
    public static bool operator ==(Vector a, Vector b) {
        return a.X == b.X && a.Y == b.Y;
    }
    
    public static bool operator !=(Vector a, Vector b) {
        return a.X != b.X || a.Y != b.Y;
    }
    
    public static Vector operator -(Vector a) {
        return new Vector(-a.X, -a.Y);
    }
}