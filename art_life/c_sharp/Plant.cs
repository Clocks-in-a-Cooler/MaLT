using System;

public class Plant : Entity {
    public override char Character {
        get { return '*'; } set { }
    }
    
    public Plant(Vector pos, Simulation sim, int energy = 10) : base(pos, sim, energy) {
        
    }
    
    public override void Update() {
        /*
            what a plant does:
            - 50% chance of getting 1 energy
            - if there is more than 6 energy and there are adjacent blank spaces
                - 20% chance of placing a new plant in an adjacent blank space
                - new plant gets 1/4 energy, old plant is left with 1/2 energy
        */
        Random rnd = new Random();
        if (rnd.Next() % 2 == 0) {
            this.Energy++;
        }
        
        if (this.Energy > 6 && rnd.Next() % 5 == 0 && this.Find_Blank_Spaces().Length > 0) {
            Vector[] blank_spaces = this.Find_Blank_Spaces();
            int space_index = rnd.Next(0, blank_spaces.Length);
            this.Reproduce(blank_spaces[space_index]);
        }
    }
    
    public override void Reproduce(Vector pos) {
        this.Sim.Get_Tile(pos.X, pos.Y).Move_Into(new Plant(pos, this.Sim, this.Energy / 4));
        this.Energy /= 2;
    }
}