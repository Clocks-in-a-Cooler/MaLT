import Foundation

// we'll be needing these
func add(_ x : Double, _ y : Double) -> Double {
    return x + y
}

func expected_frequency(column_total : Double, row_total : Double, total : Double) -> Double {
    return row_total * column_total / total
}

func chi(observed_frequency : Double, expected_frequency : Double) -> Double {
    return pow((observed_frequency - expected_frequency), Double(2)) / expected_frequency
}

func chi_squared(table: [[Double]]) -> (chi_squared: Double, f_e: [[Double]], rows: [Double], columns: [Double]) {
    // assume that the arrays are consistent
    var row_totals : [Double]    = []
    var column_totals : [Double] = [];
    
    // find the row and column totals
    for row in table {
        row_totals.append(row.reduce(0, { (x : Double, y : Double) -> Double in
            return x + y
        }))
    }
    for i in 0..<(table[0].count) {
        column_totals.append(0)
        for row in table {
            column_totals[i] += row[i]
        }
    }
    
    var total             = row_totals.reduce(0, add)
    var chi_squared_total = Double(0);
    
    var expected_frequencies : [[Double]] = [[]]
    
    // calculate and add chi squared totals
    for y in 0..<(table.count) {
        var f : [Double] = []
        for x in 0..<(table[0].count) {
            var e = expected_frequency(column_total: column_totals[x], row_total: row_totals[y], total: total)
            f.append(e)
            chi_squared_total += chi(observed_frequency: table[y][x], expected_frequency: e)
        }
        expected_frequencies.append(f)
    }
    
    var _ = expected_frequencies.remove(at: 0)
    
    return (chi_squared: chi_squared_total, f_e: expected_frequencies, rows: row_totals, columns: column_totals)
}

func generate_table(data: [String]) -> [[Double]] {
    var table : [[Double]] = [[]]
    
    for line in data {
        let elements             = line.components(separatedBy: ",")
        var table_row : [Double] = []
        
        if line.trimmingCharacters(in: .whitespaces) == "" {
            // can't stand this blank line bs that comes at the end of every file
            continue
        }
        
        for n in elements {
            table_row.append(Double(n)!)
        }
        table.append(table_row)
    }
    
    var _ = table.remove(at: 0)
    
    return table
}