//holds an x-y pair
function Vector(x, y) {
    this.x = x;
    this.y = y;
    this.length = Math.hypot(x, y);
}

Vector.prototype.plus = function(other) {
    return new Vector(this.x + other.x, this.y + other.y);
};

Vector.prototype.times = function(factor) {
    return new Vector(this.x * factor, this.y * factor);
};

// ----------------------------------------------------------
//circles!
function Circle(centre, radius) {
    this.centre = centre;
    this.radius = radius;
}

Circle.prototype.is_colliding = function(other) {
    var min_dist = this.radius + other.radius;
    var distance = Math.hypot(this.centre.x - other.centre.x, this.centre.y - other.centre.y);
    return min_dist >= distance;
};

// ----------------------------------------------------------
var Angle = {
    reduce: function(a) {
        while (a > 2 * Math.PI) {
            a -= 2 * Math.PI;
        }
        
        while (a < 0) {
            a += 2 * Math.PI;
        }
        
        return a;
    },
    
    compare: function (a, b) {
        a = this.reduce(a);
        b = this.reduce(b);
        
        if (Math.abs(a - b) < Math.PI) {
            return 1;
        } else {
            return -1;
        }
    },
    
    is_in_interval: function(min, max, angle) {
        if (min < max) {
            return angle > min && angle < max;
        } else {
            return angle > min || angle < max;
        }
    },
};
