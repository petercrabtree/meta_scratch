public class TestPhotography
{
    public void Test()
    {
        (int, string, int, int, int)[] tests = new (int, string, int, int, int)[]
        {
            (5, "APABA", 1, 2, 1),
            (5, "APABA", 2, 3, 0),
            (8, ".PBAAP.B", 1, 3, 3)
        };

        var s = new Solution();
        foreach (var test in tests)
        {
            int expected = test.Item5;
            var result = s.getArtisticPhotographCount(test.Item1, test.Item2, test.Item3, test.Item4);
            Console.WriteLine($"Parameters: ({test.Item1}, \"{test.Item2}\", {test.Item3}, {test.Item4})");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Test passed: {result == expected}\n");
        }
    }
}
