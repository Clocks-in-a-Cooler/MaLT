/*
    click and drag + selection support, a much-needed feature
    it's bulky for now, but it'll be optimized later.
    just gotta get it working, dammit.
*/

var canvas, context;
var width, height;

var current_div, select_div;

var sidepanel;

function init() {
    canvas  = document.querySelector("canvas");
    context = canvas.getContext("2d");
    
    var canvas_holder = document.getElementById("canvas-wrapper");
    
    canvas.width  = width = canvas_holder.clientWidth;
    canvas.height = height = canvas_holder.clientHeight;
    
    sidepanel   = document.getElementById("sidepanel");
    current_div = document.getElementById("current");
    select_div  = document.getElementById("selected");
    
    viewport.width  = Math.ceil(width / scale);
    viewport.height = Math.ceil(height / scale);
    
    //register event handlers for mouse
    var cw = document.getElementById("canvas-wrapper");
    cw.addEventListener("mousedown", mousedown);
    cw.addEventListener("mouseup", mouseup);
    cw.addEventListener("mousemove", mousemove);
    cw.addEventListener("click", click);
    cw.addEventListener("wheel", wheel);
    
    generate_map();
    requestAnimationFrame(animate);
}

// pushy scroll stuff ----------------------------------------------------------
// courtesy of Stack Overflow

function find_scroll_direction(e) {
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
}

// map stuff -------------------------------------------------------------------

var grid  = [];
var scale = 20;

var map_width  = 1000;
var map_height = 1000;

var selected_tile = { x: null, y: null };

function generate_map() {
    while (grid.length < map_width * map_height) {
        if (Math.random() < 0.1) {
            grid[grid.length] = "wall";
        } else {
            grid[grid.length] = "blank";
        }
    }
}

function get_tile(x, y) {
    if (x < 0 || y < 0 || x > map_width || y > map_height) return undefined;
    else return grid[y * map_width + x];
}

// viewport and getting stuff in view ------------------------------------------
var viewport = {
    x: 100,
    y: 100,
    width,
    height,
}

var dragging = false;

function draw(context) {
    context.clearRect(0, 0, width, height);
    
    //gets part of the grid that's in view
    var viewable = [];
    var tl = {
        x: Math.floor(viewport.x),
        y: Math.floor(viewport.y),
    };
    
    var br = {
        x: Math.ceil(viewport.x + viewport.width),
        y: Math.ceil(viewport.y + viewport.height),
    };
    
    var offset = {
        x: viewport.x - tl.x,
        y: viewport.y - tl.y,
    };
    
    var w = Math.ceil(br.x - tl.x);
    var h = Math.ceil(br.y - tl.y);
    
    //get all of the viewable tiles
    for (var a = tl.y; a < tl.y + h; a++) {
        for (var b = tl.x; b < tl.x + w; b++) {
            if (get_tile(b, a) != undefined) {
                viewable.push(get_tile(b, a));
            } else {
                viewable.push("blank");
            }
        }
    }
    
    //draw the grid first
    //start at offset
    context.strokeStyle = "lightslategray";
    context.lineWidth   = 0.5;
    for (var c = 0 - offset.x * scale; c < width; c += scale) {
        context.beginPath();
        context.moveTo(c, 0);
        context.lineTo(c, height);
        context.stroke();
        context.closePath();
    }
    
    for (var d = 0 - offset.y * scale; d < height; d += scale) {
        context.beginPath();
        context.moveTo(0, d);
        context.lineTo(width, d);
        context.stroke();
        context.closePath();
    }
    
    context.fillStyle = "darkorange";
    //draw the tiles!
    for (var e = 0; e < h; e++) {
        for (var f = 0; f < w; f++) {
            if (viewable[e * w + f] == "blank") continue;
            //otherwise, it's a wall tile.
            context.fillRect(
                (f - offset.x) * scale,
                (e - offset.y) * scale,
                scale, scale
            );
        }
    }
    
    //draw the highlighted square
    var cx = Math.floor(cursor.x / scale + offset.x)
    var cy = Math.floor(cursor.y / scale + offset.y);
    
    context.fillStyle = "rgba(70, 130, 180, 0.2)";
    context.fillRect(
        (cx - offset.x) * scale,
        (cy - offset.y) * scale,
        scale, scale
    );
    
    current_div.innerHTML = "cursor is over: (" +
        (cx + tl.x) + ", " + (cy + tl.y) + ").";
    
    //draw the selected square
    context.strokeStyle = "crimson";
    context.lineWidth   = 2;
    context.strokeRect(
        (selected_tile.x - tl.x - offset.x) * scale,
        (selected_tile.y - tl.y - offset.y) * scale,
        scale, scale
    );
}

// animation cycle and timing --------------------------------------------------
var last_time = null, lapse = 0;
var paused    = false;
function animate(time) {
    if (last_time == null) {
        lapse = 0;
    } else {
        lapse = time - last_time;
    }
    last_time = time;
    
    if (!paused) {
        cycle(lapse);
    }
    
    //draw a frame...
    draw(context);
    requestAnimationFrame(animate);
}

function cycle(lapse) {
    //update everything
}

// mouse event handling --------------------------------------------------------
var clicking = false;
var cursor   = {
    x: 0,
    y: 0,
};

function mousedown(e) {
    e.preventDefault();
    e.stopPropagation();

    clicking = true;
}

function mouseup(e) {
    e.preventDefault();
    e.stopPropagation();

    clicking = false;
}

function mousemove(e) {
    e.preventDefault();
    e.stopPropagation();
    
    if (clicking) {
        viewport.x -= e.movementX / scale;
        viewport.y -= e.movementY / scale;
    }
    
    cursor.x = e.offsetX;
    cursor.y = e.offsetY;
}

function click(e) {
    var tl = {
        x: Math.floor(viewport.x),
        y: Math.floor(viewport.y),
    };
    
    var offset = {
        x: viewport.x - tl.x,
        y: viewport.y - tl.y,
    };
    
    var cx = Math.floor(e.offsetX / scale + offset.x)
    var cy = Math.floor(e.offsetY / scale + offset.y);
    
    console.log("click at (" + (cx + tl.x) + ", " + (cy + tl.y) + ").");
    select_div.innerHTML =
        "clicked on (" + (cx + tl.x) + ", " + (cy + tl.y) + ")";
    select_div.innerHTML += "<br />tile is a " + get_tile(cx + tl.x, cy + tl.y);
    selected_tile.x = cx + tl.x;
    selected_tile.y = cy + tl.y;
}

function wheel(e) {
    var cursor = {
        x: e.offsetX,
        y: e.offsetY,
    };
    
    var mouse_centre = {
        x: cursor.x / scale + viewport.x,
        y: cursor.y / scale + viewport.y,
    };
    
    if (find_scroll_direction(e) == "down") {
        scale -= 0.5;
        scale = Math.max(5, scale);
    } else {
        scale += 0.5;
        scale = Math.min(scale, 50);
    }
    
    //centre it on the mouse
    viewport.x = mouse_centre.x - (cursor.x / scale);
    viewport.y = mouse_centre.y - (cursor.y / scale);
    
    viewport.width  = Math.floor(width / scale);
    viewport.height = Math.floor(height / scale);
}