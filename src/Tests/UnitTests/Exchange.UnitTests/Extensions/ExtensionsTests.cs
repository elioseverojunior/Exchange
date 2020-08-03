using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Exchange.Core.Extensions;
using Exchange.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.UnitTests.Extensions
{
    public class StringExtensionsTests : TestOutputHelper
    {
        public StringExtensionsTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] {"", true},
                new object[] {null, true},
                new object[] {" ", true},
                new object[] {"Exchange", false},
            };

        [Theory]
        [MemberData(nameof(TestData))]
        [Trait("Extensions","StringExtensions")]
        public void Given_a_String_Check_If_IsNullOrEmptyOrWhiteSpace(string data, bool expected)
        {
            try
            {
                var result = data.IsNullOrEmptyOrWhiteSpace();
                _testOutputHelper.WriteLine($"Given \"{data}\". Is it null or empty or whitespace? A: {result}");
                Assert.Equal(expected, result);
            }
            catch (Exception e)
            {
                _testOutputHelper.WriteLine(e.Message);
                throw;
            }
        }
    }
}