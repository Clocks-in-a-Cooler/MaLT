var canvas = document.querySelector("canvas");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;
var cxt = canvas.getContext("2d");

var last_time, lapse;
function animate(time) {
    if (last_time == null) {
        lapse = 0;
    } else {
        lapse = time - last_time;
    }
    last_time = time;
    
    //update...
    boats.forEach(b => { b.update(lapse); });
    
    draw();
    requestAnimationFrame(animate);
}

function draw() {
    cxt.clearRect(0, 0, window.innerWidth, window.innerHeight);
    cxt.fillStyle = "powderblue";
    cxt.fillRect(0, 0, window.innerWidth, window.innerHeight);
    boats.forEach((b) => {
        cxt.save();
        cxt.fillStyle = b.colour;
        cxt.translate(b.pos.x, b.pos.y);
        cxt.rotate(b.angle);
        cxt.beginPath();
        cxt.arc(b.width / 2, 0, b.width, 2 * Math.PI / 3, 4 * Math.PI / 3);
        cxt.arc(b.width / -2, 0, b.width, -Math.PI / 3, Math.PI / 3);
        cxt.closePath();
        cxt.fill();
        
        //draw the collision circles
        cxt.strokeStyle = "cornsilk";
        cxt.strokeWidth = 2;
        cxt.beginPath();
        cxt.arc(b.width / 2, 0, b.width, 0, Math.PI * 2);
        cxt.arc(-b.width / 2, 0, b.width, 0, Math.PI * 2);
        cxt.closePath();
        cxt.stroke();
        
        cxt.restore();
    });
}

var boats = [];

function Boat(pos, width) {
    this.pos = pos;
    this.angle = Math.random();
    this.width = width;
    this.controlling = false; //crappy crappy code!
    this.colliding = false;
    this.default_colour = "saddlebrown";
}

Boat.prototype.move_speed = 0.3;
Boat.prototype.friction = 0.03;
Boat.prototype.power = 0.01;
Boat.prototype.rot_speed = Math.PI / 2000;

Boat.prototype.control = function() {
    this.controlling = true;
    this.default_colour = "darkorange";
};

Boat.prototype.get_circles = function() {
    var offsets = new Vector(Math.cos(this.angle), Math.sin(this.angle));
    var pos_1 = this.pos.plus(offsets.times(-this.width * 0.5));
    var pos_2 = this.pos.plus(offsets.times(this.width * 0.5));
    return [
        new Circle(pos_1, this.width),
        new Circle(pos_2, this.width),
    ];
};

Boat.prototype.update = function(lapse) {
    this.colour = this.default_colour;
    if (this.controlling) {
        var movement_vector = new Vector(Math.cos(this.angle + Math.PI / 2), Math.sin(this.angle + Math.PI / 2)).times(this.move_speed * lapse);
        movement_vector = movement_vector.times(keys.up ? 1 : 0 + keys.down ? -1 : 0);
        this.pos = this.pos.plus(movement_vector);
        
        var angle_change = lapse * this.rot_speed * (keys.left ? -1 : 0 + keys.right ? 1 : 0);
        this.angle += angle_change;
    }
    
    //check for collision
    boats.forEach(other => {
        if (other === this) return;
        
        var my_circles = this.get_circles();
        var other_circles = other.get_circles();
        
        if (my_circles[0].is_colliding(other_circles[0]) &&
            my_circles[0].is_colliding(other_circles[1]) &&
            my_circles[1].is_colliding(other_circles[0]) &&
            my_circles[1].is_colliding(other_circles[1])) {
            this.colour = "firebrick";
        }
    });
};

var keys = {
    up: false,
    down: false,
    left: false,
    right: false,
};

addEventListener("keydown", (evt) => {
    switch (evt.keyCode) {
        case 37:
            keys.left = true;
            break;
        case 38:
            keys.up = true;
            break;
        case 39:
            keys.right = true;
            break;
        case 40:
            keys.down = true;
            break;
    }
});

addEventListener("keyup", (evt) => {
    switch (evt.keyCode) {
        case 37:
            keys.left = false;
            break;
        case 38:
            keys.up = false;
            break;
        case 39:
            keys.right = false;
            break;
        case 40:
            keys.down = false;
            break;
    }
});

addEventListener("click", (evt) => {
    var pos = new Vector(evt.offsetX, evt.offsetY);
    boats.push(new Boat(pos, 100));
    if (boats.length == 1) boats[0].control();
});

requestAnimationFrame(animate);

