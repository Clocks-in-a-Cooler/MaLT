var path = prompt(with: "file to read from? > ")[0]

var table = generate_table(data: try!File.read_all_lines(from: path))

var results = chi_squared(table: table)

print("row totals:")

for n in results.rows {
    print("\(n)")
}

print("column totals:")

for n in results.columns {
    print("\(n)")
}

print("expected frequencies")

for n in results.f_e {
    for value in n {
        print("\(value)", terminator: ", ")
    }
    print(" ")
}

print("chi squared value: \(results.chi_squared)")