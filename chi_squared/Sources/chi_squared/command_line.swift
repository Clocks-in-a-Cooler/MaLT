import Foundation

func prompt(with ask: String = ">") -> [String] {
    var input = ""
    repeat {
        print(ask, terminator: " ")
        input = readLine()!.trimmingCharacters(in: .whitespaces)
    } while input == ""
    return input.components(separatedBy: " ")
}