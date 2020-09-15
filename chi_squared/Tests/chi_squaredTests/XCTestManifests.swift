import XCTest

#if !canImport(ObjectiveC)
public func allTests() -> [XCTestCaseEntry] {
    return [
        testCase(chi_squaredTests.allTests),
    ]
}
#endif
