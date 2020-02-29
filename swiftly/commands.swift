import Foundation

func get_commands() -> Array<String> {
    var input = ""
    repeat {
        print(">", terminator: " ")
        input = readLine() ?? ""
        input = input.trimmingCharacters(in: .whitespaces)
    } while (input == "")
    
    return input.components(separatedBy: " ")
}

func output(_ s: String) {
    print("<< " + s)
}

command_loop: while (true) {
    var input = get_commands()
    switch input[0] {
    case "print", "say":
        output(input[1])
    case "exit", "break":
        break command_loop
    case "reverse", "esrever":
        output(String(input[1].reversed()));
    default:
        print("ERROR: no command found: \"" + input [0] + "\"")
    }
}
