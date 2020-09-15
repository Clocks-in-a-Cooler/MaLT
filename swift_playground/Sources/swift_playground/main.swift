let action = prompt(with: "read or write? (R/W) >")[0].lowercased()
let path = prompt(with: "file (use absolute path) >")[0]

if action == "read" || action == "r" {
    let lines = try? File.read_all_lines(from: path)
    print(" ")
    if lines != nil {
        print(" --- FILE CONTENTS --- ")
        for l in lines! {
            print(l)
        }
    } else {
        print("error reading file.")
    }
}

if action == "write" || action == "w" {
    print("contents to write (type :end to end the input)")
    var contents = [String]()
    input_loop: repeat {
        var string = prompt().joined(separator: " ") // lol all that hard work
        if string.trimmingCharacters(in: .whitespaces) == ":end" {
            break input_loop
        }
        contents.append(string)
    } while true
    
    do {
        try File.write_lines(to: path, lines: contents)
    } catch {
        print("error writing file.")
    }
}

// it's not a bug, it's an undocumented feature!
if action == "append" || action == "a" {
    print ("contents to append (type :end to end the input)")
    var contents = [String]()
    input_loop: repeat {
        var string = prompt().joined(separator: " ")
        if string.trimmingCharacters(in: .whitespaces) == ":end" {
            break input_loop
        }
        contents.append(string)
    } while true
    
    do {
        try File.append_lines(to: path, lines: contents)
    } catch {
        print("error appending to file.")
    }
}