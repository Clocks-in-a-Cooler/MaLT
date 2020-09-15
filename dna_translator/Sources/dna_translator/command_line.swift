import Foundation

func prompt(_ ask: String = ">") -> String {
    var input = ""
    repeat {
        print(ask, terminator: " ")
        input = readLine() ?? ""
        input = input.trimmingCharacters(in: .whitespaces)
    } while (input == "")

    return input.lowercased()
}