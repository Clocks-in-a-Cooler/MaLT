var canvas = document.querySelector("canvas");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

var allowed_moves = 200;

var population = [];
var generation = 0;
var step = 0;

function spawn_point() {
    return new Vector(200, 300);
}

var goal = new Vector(1000, 300);

function Chevron(parent) {
    if (parents) {
        this.genes = mutate(parent);
    } else {
        this.genes = (function() {
            var genes = [];
            for (var c = 0; c < allowed_moves; c++) {
                genes.push(Math.random() * Math.PI * 2);
            }
            
            return genes;
        })();
    }
    
    this.position = spawn_point();
    this.hitcircle = new Circle(this.spawn, 5);
    
    this.fitness = 0;
}

Chevron.prototype.mutate = function(parent) {
    var genes = [];
    for (var c = 0; c < parent.genes.length; c++) {
        genes.push(parent.genes[c] + mutation());
    }
};

Chevron.prototype.step = function(step) {
    
};

function mutation() {
    var n = Math.random() * Math.PI / 12;
    if (Math.random() < 0.5) {
        n *= -1;
    }
    
    return n;
}

var obstacles = [
    new Circle(new Vector(500, 150), 50),
    new Circle(new Vector(500, 450), 50),
    new Circle(new Vector(300, 300), 50),
    new Circle(new Vector(700, 300), 50),
];