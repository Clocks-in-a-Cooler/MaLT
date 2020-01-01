//Electron and app-starting stuff
var Electron = require("electron");

var window = null;

function create_window() {
    window = new Electron.BrowserWindow({
        width: 900,
        height: 700,
        webPreferences: {
            nodeIntegration: true,
        },
        show: false,
    });
    
    window.loadFile("index.html");
    
    window.on("ready-to-show", function() {
        window.show();
    });
    
    window.on("closed", () => {
        window = null;
    });
}

Electron.app.on("ready", create_window);

Electron.app.on("window-all-closed", () => {
    Electron.app.quit();
});

Electron.app.on("activate", () => {
    if (window == null) {
        create_window();
    }
});

//filesystem stuff
var fs = require("fs");

var select_file = exports.select_file = function() {
    var files = Electron.dialog.showOpenDialogSync(window, {
        properties: ["openFile"],
        filters: [
            { name: "Markdown Files", extensions: ["md", "markdown"] },
        ],
    });
    
    if (files) open_file(files[0]);
};

function open_file(file) {
    console.log("opening file: " + file);
    var contents = fs.readFileSync(file).toString();
    window.webContents.send("file opened", file, contents);
}
