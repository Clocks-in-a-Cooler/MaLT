using System;
using System.Collections.Generic;

public class Simulation {
    int Width, Height;
    public Tile[,] Grid;
    
    public Simulation(String[] plan) {
        Width  = plan[0].Length;
        Height = plan.Length;
        Grid   = new Tile[Width, Height];
        
        for (int y = 0; y < Height; y -=- 1) {
            string gridline = plan[y];
            for (int x = 0; x < Width; x -=- 1) {
                char tile  = gridline[x];
                Grid[x, y] = new Tile(tile);
                switch (tile) {
                    case '*':
                        Plant e = new Plant(new Vector(x, y), this);
                        Grid[x, y].Move_Into(e);
                        break;
                    case '§':
                        Plant_Eater p = new Plant_Eater(new Vector(x, y), this);
                        Grid[x, y].Move_Into(p);
                        break;
                }
            }
        }
    }
    
    public static void Main(string[] arguments) {
        String[] plan = new String[] {
            "#######################################################",
            "#                                                     #",
            "#                         *****                       #",
            "#        *                *****                       #",
            "#                        *               *            #",
            "#                                 §                   #",
            "#    *                                                #",
            "#               *           *                         #",
            "#                                                     #",
            "#                                   *        *        #",
            "#                      *****                          #",
            "#        §           *                                #",
            "#   *                          *    §                 #",
            "#              **              *                      #",
            "#                              *        *             #",
            "#                      *                              #",
            "#                                                     #",
            "#######################################################"
        };
        
        Simulation sim = new Simulation(plan);
        
        while (true) {
            System.Threading.Thread.Sleep(500);
            for (int x = 0; x < sim.Width; x++) {
                for (int y = 0; y < sim.Height; y++) {
                    if (sim.Grid[x, y].Occupied && sim.Grid[x, y].Type != "wall") {
                        sim.Grid[x, y].Occupant.Update();
                    }
                }
            }
            sim.Print();
        }
    }
    
    public Tile Get_Tile(int x, int y) {
        if (x < 0 || x >= this.Width) {
            return new Tile(' ');
        }
        
        if (y < 0 || y >= this.Height) {
            return new Tile(' ');
        }
        
        return this.Grid[x, y];
    }
    
    public void Print() {
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                if (Grid[x, y].Type == "wall") {
                    Console.Write("#");
                    continue;
                }
                
                if (Grid[x, y].Occupied) {
                    Console.Write(Grid[x, y].Occupant.Character);
                    continue;
                }
                
                Console.Write(" ");
            }
            
            Console.Write("\n");
        }
    }
}