using System;
using System.Collections.Generic;

public abstract class Entity {
    public Vector Pos;
    public int Energy;
    public Simulation Sim;
    public abstract char Character { get; set; }
    
    public Entity(Vector pos, Simulation sim, int energy = 2) {
        this.Pos    = pos;
        this.Sim    = sim;
        this.Energy = energy;
    }
    
    public abstract void Update();
    
    public abstract void Reproduce(Vector pos);
    
    public Vector[] Find_Blank_Spaces() {
        int blank_spaces = 0;
        
        // first count the number of blank spaces
        // then initiate an array with that size
        // then add the blank positions to that array
        
        // far more convoluted than it needs to be
        // probably easier to use an arraylist
        
        for (int x = this.Pos.X - 1; x < this.Pos.X + 2; x++) {
            for (int y = this.Pos.Y - 1; y < this.Pos.Y + 2; y++) {
                if (!this.Sim.Get_Tile(x, y).Occupied) {
                    blank_spaces++;
                }
            }
        }
        
        Vector[] blank_positions = new Vector[blank_spaces];
        
        int pos = 0;
        
        for (int x = this.Pos.X - 1; x < this.Pos.X + 2; x++) {
            for (int y = this.Pos.Y - 1; y < this.Pos.Y + 2; y++) {
                if (!this.Sim.Get_Tile(x, y).Occupied) {
                    blank_positions[pos] = new Vector(x, y);
                    pos++;
                }
            }
        }
        
        return blank_positions;
    }
}