using Base64Encoder.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Base64Encoder.UnitTest.ServiceUnitTest
{
    public class EncoderServiceUnitTest
    {
        private readonly IEncoderService _service;
        public EncoderServiceUnitTest(IEncoderService service)
        {
            _service = service;
        }

        [Fact]
        public async Task Encode_ReturnCharactersInCharacterHandler()
        {
            // Arrange
            var text = "Hello, World!";
            var resultBuilder = new StringBuilder();

            async Task CharacterHandler(char character)
            {
                resultBuilder.Append(character);
                await Task.Delay(2000); // Dummy delay
            }

            var cancellationToken = new CancellationToken();

            // Act
            await _service.Encode(text, CharacterHandler, cancellationToken);

            // Assert
            var result = resultBuilder.ToString();
            Assert.Equal(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)), result);
        }

        [Fact]  
        public async Task Encode_ShouldHandleCancellation()
        {
            // Arrange
            var text = "Hello, World!";
            var resultBuilder = new StringBuilder();

            async Task CharacterHandler(char character)
            {
                resultBuilder.Append(character);
                await Task.Delay(2000); // Dummy delay
            }

            var cancellationToken = new CancellationTokenSource();
            cancellationToken.Cancel(); // Cancel immediately

            // Act
            await _service.Encode(text, CharacterHandler, cancellationToken.Token);

            // Assert
            var result = resultBuilder.ToString();
            Assert.Equal(" ", result);
        }
    }
}
