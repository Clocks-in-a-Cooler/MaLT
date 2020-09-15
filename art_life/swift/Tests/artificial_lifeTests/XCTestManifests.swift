import XCTest

#if !canImport(ObjectiveC)
public func allTests() -> [XCTestCaseEntry] {
    return [
        testCase(artificial_lifeTests.allTests),
    ]
}
#endif
