using FluentAssertions;
using FluentValidation.TestHelper;
using TodoApi.Application.Models;
using Xunit;

namespace TodoAPI.Test.Unit
{
    public class TodoRequestValidatorTests
    {
        private readonly TodoRequestValidator _validator;

        public TodoRequestValidatorTests() 
            => _validator = new TodoRequestValidator();

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Validate_When_name_is_required_Should_fail(string name)
        {
            // Arrange
            var request = new TodoRequest { Name = name };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Name");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abcd")]
        public void Validate_When_name_is_valid_Should_pass(string name)
        {
            // Arrange
            var request = new TodoRequest { Name = name };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}