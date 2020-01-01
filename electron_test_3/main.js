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