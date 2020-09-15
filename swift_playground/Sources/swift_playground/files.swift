import Foundation

//shush, i'm just getting started
func read_string_from_file(at path: String) -> String {
    let file = FileHandle(forUpdatingAtPath: path)
    let data = try! file!.readToEnd()!
    return String(decoding: data, as: UTF8.self)
}

func write_string_to_file(at path: String, contents: String) {
    let data = contents.data(using: .utf8)!
    let file = FileHandle(forUpdatingAtPath: path)
    try! file!.write(data)
}
