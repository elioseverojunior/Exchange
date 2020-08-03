using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Exchange.UnitTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.UnitTests
{
    public class CreateTableDto
    {
        [Required] [StringLength(50)] public string Name { get; set; }

        [StringLength(200)] public string Description { get; set; }

        [Required] [Range(0, 100)] public int? MaxPartySize { get; set; }
    }

    public class CreateTableDtoValidator : AbstractValidator<CreateTableDto>
    {
        public CreateTableDtoValidator()
        {
            RuleFor(t => t.Name).NotEmpty().Length(1, 50);
            RuleFor(t => t.Description).Length(0, 200);
            RuleFor(t => t.MaxPartySize).NotNull().GreaterThan(0).LessThanOrEqualTo(100);
        }
    }

    public class FluentValidationTests : TestOutputHelper
    {
        public FluentValidationTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void FluentValidationTest()
        {
            var validator = new CreateTableDtoValidator();

            var table = new CreateTableDto
            {
                Name = "Table 1",
                Description = new string('a', 201),
                MaxPartySize = null
            };

            var validationResult = validator.Validate(table);

            Assert.False(validationResult.IsValid);
        }
    }
}