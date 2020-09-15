import Foundation

// prompt for a DNA sequence, transcribe it to RNA, then translate it to a peptide chain

// first, determine if the input strands will be coding or template strand

var strand_type : String

if CommandLine.arguments.count < 2 || CommandLine.arguments[1] != "coding" {
    strand_type = "template"
} else {
    strand_type = "coding"
}

program_loop: repeat {
    var sequence = prompt("DNA sequence (\(strand_type))>")
    
    if sequence == "exit" {
        break program_loop
    }
    
    var transcribed = transcribe(sequence, strand_type == "coding")
    print("mRNA sequence: \(transcribed)")
    
    var polypeptide = translate(transcribed)
    print("polypeptide produced: \(polypeptide)")
    
    print(" ") // insert a blank line
} while true