using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
// ReSharper disable InconsistentNaming

namespace Exchange.UnitTests.Helpers
{
    [ExcludeFromCodeCoverage]
    public class TestOutputHelper
    {
        protected readonly ITestOutputHelper _testOutputHelper;

        protected TestOutputHelper(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}