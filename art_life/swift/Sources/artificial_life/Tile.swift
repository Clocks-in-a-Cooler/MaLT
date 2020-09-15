import Foundation

var energy_key = [
    " " : 0,
    "#" : 0,
    "*" : 10,
    "o" : 30
]

class Tile : Equatable {
    var character : String
    var sim : Simulation
    var pos : Vector
    var energy : Int
    
    init(tile : String, sim : Simulation, pos : Vector) {
        self.character = tile
        self.sim       = sim
        self.pos       = pos
        self.energy    = energy_key[tile]!
    }
    
    func update() -> Void {
        // don't want to write code like Yanderedev, but for this it's okay since there are only a few types
        // probably find some way to refactor this
        if self.character == " " || self.character == "#" {
            return
        }
        
        if self.character == "*" {
            // see Plant.cs for the plant's "ai" -- in any case, this plant is smarter than me
            if Bool.random() {
                self.energy += 1
            }
            
            if Int.random(in: 1...5) == 5 && self.energy > 6 && !find_blank_spaces().isEmpty {
                // reproduce
                var tile = find_blank_spaces().randomElement()!
                
                tile.fill(new_tile: self.character, new_energy: Int(self.energy / 4))
                self.energy = Int(self.energy / 2)
            }
        }
        
        if self.character == "§" {
            // see Plant_Eater.cs for the herbivore's "ai"
        }
    }
    
    func find_blank_spaces() -> [Tile] {
        return self.find_spaces(test: { (tile : Tile) -> Bool in
            return tile.character == " "
        })
    }
    
    func find_spaces(test : (Tile) -> Bool) -> [Tile] {
        var tiles = [Tile]()
        for y in (self.pos.y - 1)...(self.pos.y + 1) {
            for x in (self.pos.x - 1)...(self.pos.x + 1) {
                var tile = self.sim.get_tile(x, y)
                if test(tile) && tile != self {
                    tiles.append(tile)
                }
            }
        }
        
        return tiles
    }
    
    func fill(new_tile : String, new_energy : Int) -> Void {
        self.character = new_tile; self.energy = new_energy
    }
    
    func leave(to other_tile : Tile) -> Void {
        other_tile.fill(new_tile: self.character, new_energy: self.energy)
        self.empty()
    }
    
    func empty() -> Void {
        self.character = " "; self.energy = 0
    }
    
    static func == (lhs: Tile, rhs: Tile) -> Bool {
        return lhs.pos == rhs.pos
    }
}