var {app, BrowserWindow} = require('electron')

var main_window;

function create_window () {
    main_window = new BrowserWindow({
        width: 800,
        height: 600,
        webPreferences: {
            nodeIntegration: true,
        },
    });

    main_window.loadFile("index.html");
    
    main_window.on("closed", function() {
        main_window = null;
    });
}

app.on('ready', create_window);

app.on('window-all-closed', function () {
    if (process.platform !== 'darwin') {
        app.quit()
    }
})

app.on('activate', function () {
    if (main_window === null) {
        create_window();
    }
});