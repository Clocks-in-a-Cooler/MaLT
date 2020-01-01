//first, let's set some elements up.
var save_button = document.getElementById("save_file");
var open_file = document.getElementById("open_file");
var editor = document.getElementById("editor");

//the file we're currently working with
var current_file = { name: "" };

//set up CodeMirror
var codemirror = CodeMirror(editor, {
    mode: "javascript",
});

//set up event listeners
save_button.addEventListener("click", function(e) {
    download(current_file.name, codemirror.doc.getValue());
});

open_file.addEventListener("change", function() {
    if (open_file.files.length > 0) {
        current_file = open_file.files[0];
        console.log("opening file: " + current_file.name + "...");
        
        var reader = new FileReader();
        
        reader.addEventListener("load", function() {
            codemirror.doc.setValue(reader.result);
        });
        reader.readAsText(current_file);
    }
});

//helper functions

//download the file, since we can't save it directly.
function download(filename, text) {
    //courtesty of Stack Overflow.
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}
