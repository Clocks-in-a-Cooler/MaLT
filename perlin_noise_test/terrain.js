/*
    based on the tutorial at:
    https://flafla2.github.io/2014/08/09/perlinnoise.html
*/
var random_numbers = [46,86,198,189,27,192,142,215,173,45,90,226,226,90,181,74,197,211,74,182,151,210,126,174,
    219,33,194,34,184,22,149,212,231,202,230,67,62,65,145,27,20,228,152,122,218,222,230,254,157,158,134,46,112,
    81,88,196,24,151,139,70,254,74,231,191,238,12,122,4,32,103,116,11,174,9,24,10,202,116,163,54,16,49,222,160,
    180,37,205,101,57,106,50,3,193,171,172,209,196,136,135,88,90,250,16,49,200,113,236,132,239,195,33,175,123,
    124,178,29,168,118,43,84,219,111,4,89,0,232,4,156,210,113,72,129,191,223,200,186,98,101,216,201,101,151,178,
    204,114,76,1,122,154,148,31,130,113,42,40,212,182,43,255,220,15,9,129,112,162,203,17,196,138,204,24,105,17,
    72,190,91,78,208,88,179,239,231,91,167,226,86,196,52,87,90,66,216,64,166,137,192,243,55,70,2,233,105,134,
    159,249,55,169,37,114,177,161,162,182,142,159,62,113,188,21,186,108,255,68,202,18,232,222,214,117,49,96,54,
    135,92,210,96,195,137,58,202,4,223,6,117,132,118,46,234,99,245,232,120,191,3,51,83
];

function Vector(x, y) {
    this.x = x; this.y = y;
}

Vector.prototype.dot_product = function(other) {
    return other.x * this.x + other.y * this.y;
};

function fade(x) {
    //fade function
    return 6 * (x ** 5) - 15 * (x ** 4) + 10 * (x ** 3);
}

function hash(x) {
    return random_numbers[x];
}

function increment(x) {
    return (x + 1) % 256;
}

function grad(hash, x, y) {
    switch (hash % 4) {
        case 0:
            return -x;
        case 1:
            return x;
        case 2:
            return y;
        case 3:
            return -y;
    }
}

function linear_interpolate(a, b, x) {
    return a + x * (b - a);
}

function noise(x, y) {
    x = x % 256, y = y % 256;

    //get the integer coordinates
    var int_x = Math.floor(x), int_y = Math.floor(y);
    var float_x = x - int_x, float_y = y - int_y;

    //get the fade
    var fade_x = fade(float_x), fade_y = fade(float_y);
    
    //get the hash
    var hashes = [
        hash(hash(int_x           ) +           int_y),
        hash(hash(increment(int_x)) +           int_y),
        hash(hash(int_x           ) + increment(int_y)),
        hash(hash(increment(int_x)) + increment(int_y)),
    ];
    
    //do some weird hashing stuff
    var a1 = linear_interpolate(grad(hashes[0], float_x, float_y),
                                grad(hashes[2], float_x - 1, float_y), fade_x);
    var a2 = linear_interpolate(grad(hashes[1], float_x, float_y - 1),
                                grad(hashes[3], float_x - 1, float_y - 1), fade_x);
    /*var b1 = */ return linear_interpolate(a1, a2, fade_y);
    /*
    var a3 = linear_interpolate(grad(hashes[0], float_x, float_y - 1),
                                grad(hashes[2], float_x - 1, float_y - 1), fade_x);
    var a4 = linear_interpolate(grad(hashes[1], float_x, float_y - 1),
                                grad(hashes[3], float_x - 1, float_y - 1), fade_x);
    var b2 = linear_interpolate(a3, a4, fade_y);
    
    return linear_interpolate(b1, b2, fade_x);*/
}

function get_tile_type(x, y) {
    var num = noise(x, y);
    if (num < -0.5) {
        return "midnightblue";
    } else if (num < -0.25) {
        return "blue";
    } else if (num < 0) {
        return "dodgerblue";
    } else if (num < 0.25) {
        return "deepskyblue";
    } else if (num < 0.5) {
        return "powderblue";
    } else {
        return "aliceblue";
    }
}

//draw!
var canvas = document.querySelector("canvas");
canvas.width = canvas.height = 600;
var cxt = canvas.getContext("2d");

var width = 100, height = 100;
var zoom = 3;

for (var y = 0; y < height; y++) {
    for (var x = 0; x < width; x++) {
        cxt.fillStyle = get_tile_type(x / width * zoom, y / height * zoom);
        cxt.fillRect(x * 6, y * 6, 6, 6);
    }
}