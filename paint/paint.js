var image_layer = document.getElementById("image layer");
var image_cxt = image_layer.getContext("2d");

var control_layer = document.getElementById("control layer");
var control_cxt = control_layer.getContext("2d");

//resize the canvases, since i still can't figure out how with css
image_layer.width = control_layer.width = window.innerWidth;
image_layer.height = control_layer.height = window.innerHeight;

image_cxt.strokeStyle = "dodgerblue";
image_cxt.lineWidth = 2;

var draw_size = 1;

var mouse = {
    clicking: false,
    pos: {x: null, y: null},
    click: function() {
        console.log("mouse click at (" + this.pos.x + ", " + this.pos.y + ")");
    },
    scroll: function(evt) {
        var direction = this.find_scroll_direction(evt);
        if (direction == "down") {
            draw_size -= 0.1;
            draw_size = Math.max(draw_size, 0.2);
        } else {
            draw_size += 0.1;
            draw_size = Math.min(draw_size, 5);
        }
    },
    // courtesy of Stack Overflow
    find_scroll_direction: function(e) {
        var delta;

        if (e.wheelDelta){
            delta = e.wheelDelta;
        } else {
            delta = -1 * e.deltaY;
        }

        if (delta < 0){
            return "down";
        } else if (delta > 0){
            return "up";
        }
    },
};

addEventListener("mousedown", function() {
    mouse.clicking = true;
});

addEventListener("mouseup", function() {
    mouse.clicking = false;
});

addEventListener("mousemove", function(evt) {
    mouse.pos = { x: evt.clientX, y: evt.clientY };
});

addEventListener("click", (evt) => { mouse.click(); });

window.addEventListener("wheel", (evt) => { mouse.scroll(evt); }, false);

addEventListener("keyup", function(evt) {
    var controls = document.getElementById("controls");
    if (evt.keyCode == 32) {
        controls.style.visibility = controls.style.visibility == "hidden" ? "visible" : "hidden";  
    }
    
    if (evt.keyCode == 13) {
        save_image();
    }
});

var tools = {
    "pencil": {
        last_pos: null,
        size: 2,
        update: function() {
            if (mouse.clicking) {
                if (this.last_pos != null) {
                    //draw!
                    image_cxt.lineWidth = this.size * draw_size;
                    image_cxt.beginPath();
                    image_cxt.moveTo(this.last_pos.x, this.last_pos.y);
                    image_cxt.lineTo(mouse.pos.x, mouse.pos.y);
                    image_cxt.closePath();
                    image_cxt.stroke();
                    
                    //had to fix something
                    image_cxt.fillStyle = image_cxt.strokeStyle;
                    image_cxt.beginPath();
                    image_cxt.moveTo(this.last_pos.x, this.last_pos.y);
                    image_cxt.arc(this.last_pos.x, this.last_pos.y,
                        this.size * draw_size * 0.5, 0, Math.PI * 2);
                    image_cxt.moveTo(mouse.pos.x, mouse.pos.y);
                    image_cxt.arc(mouse.pos.x, mouse.pos.y,
                        this.size * draw_size * 0.5, 0, Math.PI * 2);
                    image_cxt.closePath();
                    image_cxt.fill();
                }
                
                this.last_pos = mouse.pos;
            } else {
                this.last_pos = null;
            }
            
            //draw the pencil
            control_cxt.save();
            control_cxt.fillStyle = "black";
            control_cxt.translate(mouse.pos.x, mouse.pos.y);
            control_cxt.beginPath();
            control_cxt.lineTo(0, -5);
            control_cxt.lineTo(25, -30);
            control_cxt.lineTo(30, -25);
            control_cxt.lineTo(5, 0);
            control_cxt.lineTo(0, 0);
            control_cxt.closePath();
            control_cxt.fill();
            control_cxt.restore();
        },
    },
    
    "eraser": {
        size: 15,
        update: function() {
            if (mouse.clicking) {
                image_cxt.save();
                image_cxt.translate(mouse.pos.x, mouse.pos.y);
                image_cxt.clearRect(-1 * this.size * draw_size,
                    -1 * this.size * draw_size,
                    2 * this.size * draw_size,
                    2 * this.size * draw_size);
                image_cxt.restore();
            }
            
            //draw the actual eraser
            control_cxt.save();
            control_cxt.strokeStyle = "black";
            control_cxt.translate(mouse.pos.x, mouse.pos.y);
            control_cxt.beginPath(); //satisfaction from the shape of the lines
            control_cxt.moveTo(-1 * this.size * draw_size, -1 * this.size * draw_size);
            control_cxt.lineTo(1 * this.size * draw_size, -1 * this.size * draw_size);
            control_cxt.lineTo(1 * this.size * draw_size, 1 * this.size * draw_size);
            control_cxt.lineTo(-1 * this.size * draw_size, 1 * this.size * draw_size);
            control_cxt.lineTo(-1 * this.size * draw_size, -1 * this.size * draw_size);
            control_cxt.closePath();
            control_cxt.stroke();
            control_cxt.restore();
        },
    },
};

var current_tool = tools["pencil"];

function create_rbutton_group(element, callback) {
    var buttons = element.childNodes;
    //nesting! fun! i code terrible!
    buttons.forEach((b) => {
        b.addEventListener("click", (evt) => {
            buttons.forEach((u) => {
                u.className = "rbutton";
            });
            
            b.className += " selected";
            callback(b.id);
        });
    });
}

create_rbutton_group(document.getElementById("colour_controls"), function(c) {
    image_cxt.strokeStyle = image_cxt.fillStyle = c;
});

create_rbutton_group(document.getElementById("tools"), function(t) {
    current_tool = tools[t];
});

var save_image = function() {
  var link = document.createElement('a');
  link.download = 'whiteboard.png';
  link.href = image_layer.toDataURL()
  link.click();
}

//cycling and updating the canvas
var last_time = null, lapse = 0;
function cycle(time) {
    control_cxt.clearRect(0, 0, window.innerWidth, window.innerHeight);
    current_tool.update();
    requestAnimationFrame(cycle);
}

requestAnimationFrame(cycle);

