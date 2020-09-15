using System;
using System.Collections.Generic;

public class Tile {
    static Dictionary<char, string> Tile_types = new Dictionary<char, string> {
        ['#'] = "wall",
        [' '] = "blank",
    };
    
    public string Type;
    public Entity Occupant;
    
    public Tile(char t) {
        // base class for a tile
        if (!Tile_types.TryGetValue(t, out this.Type)) {
            this.Type = "blank";
        }
    }
    
    public bool Occupied {
        get {
            return this.Occupant != null || this.Type == "wall";
        }
    }
    
    public void Move_Into(Entity new_occupant) {
        this.Occupant = new_occupant;
    }
    
    public void Move_Out() {
        this.Occupant = null;
    }
}