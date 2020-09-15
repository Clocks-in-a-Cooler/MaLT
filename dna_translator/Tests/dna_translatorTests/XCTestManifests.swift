import XCTest

#if !canImport(ObjectiveC)
public func allTests() -> [XCTestCaseEntry] {
    return [
        testCase(dna_translatorTests.allTests),
    ]
}
#endif
