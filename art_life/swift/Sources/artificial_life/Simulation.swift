import Foundation

class Simulation {
    var map = [[Tile]]()
    var width : Int
    var height : Int
    
    init(plan: [String]) {
        self.width = plan[0].count; self.height = plan.count
        var x = 0, y = 0
        for gridline in plan {
            var line      = Array(gridline)
            var tile_line = [Tile]()
            for t in line {
                tile_line.append(Tile(tile: String(t), sim: self, pos: Vector(x, y)))
                x += 1
            }
            map.append(tile_line)
            x = 0
            y += 1
        }
    }
    
    func get_tile(_ x : Int, _ y : Int) -> Tile {
        if x < 0 || x >= self.width || y < 0 || y >= self.height {
            return Tile(tile: " ", sim: self, pos: Vector(0, 0))
        }
        
        return self.map[y][x]
    }
    
    func update() -> Void {
        for line in map {
            for tile in line {
                tile.update()
            }
        }
    }
    
    func print() -> Void {
        for line in map {
            for tile in line {
                Swift.print(tile.character, terminator: "")
            }
            Swift.print("", terminator: "\n")
        }
    }
}