using Exchange.Exceptions;
using Exchange.Helpers;
using Exchange.Models;

namespace ExchangeTests
{
    public class InputHandlerTest
    {
        [Fact]
        public void SplitInputTest_GoodInput_returnsTrue()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var input = "100 USD/EUR";
            var expected = new List<ExchangeRequest>
            {
                new("USD","EUR",100)
            };

            // Act
            var result = inputHandler.SplitInput(input);

            // Assert
            Assert.Equal(expected[0].InputCurrency, result[0].InputCurrency);
            Assert.Equal(expected[0].OutputCurrency, result[0].OutputCurrency);
            Assert.Equal(expected[0].OriginalAmount, result[0].OriginalAmount);
        }

        [Fact]
        public void SplitInputTest_EmptyInput_ThrowsCustomException()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var input = "";

            // Act
            CustomException exception = Assert.Throws<CustomException>(() => inputHandler.SplitInput(input));

            // Assert
            Assert.Equal("Input is empty", exception.Message);
        }
        [Fact]
        public void SplitInputTest_BadInput_ThrowsCustomException()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var input = "100 USD EUR";

            // Act
            CustomException exception = Assert.Throws<CustomException>(() => inputHandler.SplitInput(input));

            // Assert
            Assert.Equal("Input is not written in a meaningful way.", exception.Message);
        }
        [Fact]
        public void ConvertAmountTest_GoodInput_returnsTrue()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var amount = "100";

            // Act
            var result = inputHandler.ConvertAmount(amount);

            // Assert
            Assert.Equal(100, result);
        }
        [Fact]
        public void ConvertAmountTest_BadInput_returnsZero()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var amount = "100,5,5";

            // Act
            var result = inputHandler.ConvertAmount(amount);

            // Assert
            Assert.Equal(0, result);
        }


        [Fact]
        public void ConvertAmountTest_EmptyInput_returnsZero()
        {
            // Arrange
            var inputHandler = new InputHandler();
            var amount = "";

            // Act
            var result = inputHandler.ConvertAmount(amount);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
