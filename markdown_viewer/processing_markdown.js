var Electron = require("electron");
var remote = Electron.remote, ipc_renderer = Electron.ipcRenderer;
var main_process = remote.require("./main.js");

var marked = require("marked");

var open_file_button = document.getElementById("open-file");
var viewer = document.getElementById("html");

function render_markdown(markdown) {
    viewer.innerHTML = marked(markdown, { sanitize: true });
}

open_file_button.addEventListener("click", function(e) {
    main_process.select_file();
});

ipc_renderer.on("file opened", function(event, file, contents) {
    console.log("contents: " + contents);
    render_markdown(contents);
});
