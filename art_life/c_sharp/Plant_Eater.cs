using System;
using System.Collections.Generic;

public class Plant_Eater : Entity {
    public override char Character {
        get { return '§'; } set { }
    }
    
    public Plant_Eater(Vector pos, Simulation sim, int energy = 30) : base(pos, sim, energy) {
        
    }
    
    public override void Update() {
        /*
            what a herbivore does:
            - if there is 0 energy, disappear
            - if there is a plant around, eat it (gets the plant's energy, remove the plant)
            - stumble around (uses 1 energy)
            - if there is more than 75 energy and there is an empty space, reproduce (success 10%)
                - offspring gets 1/3 energy
                - parent retains 1/2 energy
        */
        
        // die
        if (this.Energy <= 0) {
            this.Sim.Grid[this.Pos.X, this.Pos.Y].Move_Out();
            return;
        }
        
        // eat
        Entity[] plants = Find_Plants();
        if (plants.Length > 0) {
            Plant plant = (Plant) Utils.Random_Element(plants);
            
            this.Energy += plant.Energy;
            this.Sim.Grid[plant.Pos.X, plant.Pos.Y].Move_Out();
            
            return;
        }
        
        // move
        if (Find_Blank_Spaces().Length > 0) {
            Vector new_pos = Utils.Random_Element(Find_Blank_Spaces());
            
            this.Sim.Grid[this.Pos.X, this.Pos.Y].Move_Out();
            this.Pos = new_pos;
            this.Sim.Grid[this.Pos.X, this.Pos.Y].Move_Into(this);
            this.Energy--;
        }
        
        // reproduce
        if (this.Energy >= 30 && Find_Blank_Spaces().Length > 0) {
            Vector[] blank_spaces = this.Find_Blank_Spaces();
            this.Reproduce(Utils.Random_Element(blank_spaces));
        }
    }
    
    public override void Reproduce(Vector pos) {
        this.Sim.Grid[pos.X, pos.Y].Move_Into(new Plant_Eater(pos, this.Sim, this.Energy / 3));
        this.Energy /= 2;
    }
    
    Entity[] Find_Plants() {
        List<Entity> plants = new List<Entity>();
        for (int x = this.Pos.X - 1; x <= this.Pos.X + 1; x++) {
            for (int y = this.Pos.Y - 1; y <= this.Pos.Y + 1; y++) {
                Tile tile = this.Sim.Get_Tile(x, y);
                if (!tile.Occupied || tile.Type == "wall") continue;
                if (tile.Occupant.Character == '*' && tile.Occupant != this) {
                    plants.Add(tile.Occupant);
                }
            }
        }
        
        return plants.ToArray();
    }
}