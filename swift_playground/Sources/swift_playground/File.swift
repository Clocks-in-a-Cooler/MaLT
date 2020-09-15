import Foundation

//the goal: provide a simpler interface for working with files. nothing too fancy.
class File {
    static let fm = FileManager.default
    
    static func read_all_lines(from path: String, as encoding: String.Encoding = .utf8) throws -> [String] {
        if !exists(at: path) {
            throw FileError.file_nonexistent
        }
        
        if !fm.isReadableFile(atPath: path) {
            throw FileError.file_unreadable
        }
        
        let data = fm.contents(atPath: path)!
        return String(data: data, encoding: encoding)!.components(separatedBy: "\n")
    }
    
    static func write_lines(to path: String, lines: [String], as encoding: String.Encoding = .utf8) throws {
        let string = lines.joined(separator: "\n")
        if !exists(at: path) {
            // don't throw error, instead create file
            let data = string.data(using: encoding)!
            fm.createFile(atPath: path, contents: data, attributes: nil)
            
            return
        }
        
        if !fm.isWritableFile(atPath: path) {
            throw FileError.file_unwritable
        }
        
        let url = URL(string: "file://" + path)!
        try! string.write(to: url, atomically: false, encoding: encoding)
    }
    
    static func append_lines(to path: String, lines: [String], as encoding: String.Encoding = .utf8) throws {
        if !exists(at: path) {
            // if file not exist, simply create it
            do {
                try write_lines(to: path, lines: lines, as: encoding)
            } catch {
                throw error // not my problem!
            }
            
            return
        }
        
        let handler = FileHandle(forUpdatingAtPath: path)!
        do {
            try handler.seekToEnd()
        } catch {
            throw error // again, not my problem!
        }
        let string = "\n" + lines.joined(separator: "\n")
        let data = string.data(using: encoding)!
        handler.write(data)
        handler.closeFile()
    }
    
    static func exists(at path: String) -> Bool {
        return fm.fileExists(atPath: path)
    }
}

public enum FileError : Error {
    case file_unreadable
    case file_unwritable
    case file_nonexistent
}