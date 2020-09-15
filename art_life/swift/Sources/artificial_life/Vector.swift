import Foundation

struct Vector : Equatable {
    var x : Int
    var y : Int
    
    init(_ x : Int, _ y : Int) {
        self.x = x; self.y = y
    }
    
    func plus(other : Vector) -> Vector {
        return Vector(self.x + other.x, self.y + other.y)
    }
    
    func times(factor : Double) -> Vector {
        return Vector(Int(Double(self.x) * factor), Int(Double(self.y) * factor))
    }
    
    static func == (lhs: Vector, rhs: Vector) -> Bool {
        return lhs.x == rhs.x && lhs.y == rhs.y
    }
}