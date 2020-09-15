func transcribe(_ bases : String, _ coding : Bool) -> String {
    var transcribed = ""
    for b in bases {
        if coding {
            // if we're given the coding strand, first convert it to the template strand
            transcribed += rna_bases[dna_bases[String(b)]!]!
        } else {
            transcribed += rna_bases[String(b)]!
        }
    }
    
    return transcribed
}

let dna_bases = [
    "a" : "t",
    "c" : "g",
    "g" : "c",
    "t" : "a",
]

let rna_bases = [
    "a" : "u",
    "c" : "g",
    "g" : "c",
    "t" : "a",
]

func translate(_ rna_sequence : String) -> String {
    var remaining     = rna_sequence
    var peptide_chain = ""
    repeat {
        var codon = String(remaining.dropLast(remaining.count - 3)) // get the first three bases of the sequence
        peptide_chain.append(codon_dictionary[codon]!)
        
        peptide_chain.append(" ") // easier to read
        
        remaining.removeFirst(3) // advance to the next codon
    } while remaining.count >= 3
    
    if (remaining.count == 1) {
        print("incomplete codon: \(remaining)")
    }
    
    if (remaining.count == 2) {
        // we might still be able to match the last one
        print("incomplete codon: \(remaining)")
        
        var amino_acid : String = two_codon_dictionary[remaining] ?? ""
        peptide_chain.append(amino_acid)
    }
    
    return peptide_chain
}

let codon_dictionary = [
    "uuu" : "Phe",
    "uuc" : "Phe",
    "uua" : "Leu",
    "uug" : "Leu",
    "ucu" : "Ser",
    "ucc" : "Ser",
    "uca" : "Ser",
    "ucg" : "Ser",
    "uau" : "Tyr",
    "uac" : "Tyr",
    "uaa" : "stop",
    "uag" : "stop",
    "ugu" : "Cys",
    "ugc" : "Cys",
    "uga" : "Stop",
    "ugg" : "Trp",
    "cuu" : "Leu",
    "cuc" : "Leu",
    "cua" : "Leu",
    "cug" : "Leu",
    "ccu" : "Pro",
    "ccc" : "Pro",
    "cca" : "Pro",
    "ccg" : "Pro",
    "cau" : "His",
    "cac" : "His",
    "caa" : "Gin",
    "cag" : "Gin",
    "cgu" : "Arg",
    "cgc" : "Arg",
    "cga" : "Arg",
    "cgg" : "Arg",
    "auu" : "Ile",
    "auc" : "Ile",
    "aua" : "Ile",
    "aug" : "Met",
    "acu" : "Thr",
    "acc" : "Thr",
    "aca" : "Thr",
    "acg" : "Thr",
    "aau" : "Asn",
    "aac" : "Asn",
    "aaa" : "Lys",
    "aag" : "Lys",
    "agu" : "Ser",
    "agc" : "Ser",
    "aga" : "Arg",
    "agg" : "Arg",
    "guu" : "Val",
    "guc" : "Val",
    "gua" : "Val",
    "gug" : "Val",
    "gcu" : "Ala",
    "gcc" : "Ala",
    "gca" : "Ala",
    "gcg" : "Ala",
    "gau" : "Asp",
    "gac" : "Asp",
    "gaa" : "Glu",
    "gag" : "Glu",
    "ggu" : "Gly",
    "ggc" : "Gly",
    "gga" : "Gly",
    "ggg" : "Gly",
]

let two_codon_dictionary = [
    // some amino acids can be identified with only two bases
    "uc" : "Ser",
    "cu" : "Leu",
    "cc" : "Pro",
    "cg" : "Arg",
    "ac" : "Thr",
    "gu" : "Val",
    "gc" : "Ala",
    "gg" : "Gly",
]