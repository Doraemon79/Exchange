using Exchange.Exceptions;
using Exchange.Logic;
using System.Collections.Frozen;

namespace ExchangeTests
{
    public class ExchangeCalculatorTests
    {

        Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>
    {
        { "GBP", 0.7543701104m },
        { "EUR", 0.8945801398m },
        { "DKK", 6.6737710158m },
        { "Tst", 2m },
        { "Tst1", 7m },
         { "Tst2", 0m }
    };



        [Fact]
        public void RateCalculator_GoodInput_ReturnsTrue()
        {
            //Arrange
            FrozenDictionary<string, decimal> FrozenRates = dictionary.ToFrozenDictionary();
            var ExchangeCalculator = new ExchangeCalculator();


            //Act
            var actualResult = ExchangeCalculator.RateCalculator("Tst", "Tst1", FrozenRates);

            //Assert
            Assert.Equal(3.5m, actualResult);
        }

        [Fact]
        public void RateCalculator_InputItself_ReturnsOne()
        {
            //Arrange
            FrozenDictionary<string, decimal> FrozenRates = dictionary.ToFrozenDictionary();
            var ExchangeCalculator = new ExchangeCalculator();


            //Act
            var actualResult = ExchangeCalculator.RateCalculator("Tst", "Tst", FrozenRates);

            //Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public void RateCalculator_CurrencyNotIncluded_ReturnsKeyNotFoundException()
        {
            //Arrange
            FrozenDictionary<string, decimal> FrozenRates = dictionary.ToFrozenDictionary();
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(() => ExchangeCalculator.RateCalculator("Tst", "Tst4", FrozenRates));

            //Assert
            Assert.Equal("The currency 'Tst4' was not found in the dictionary.", exception.Message);
        }

        [Fact]
        public void AmountCalculator_GoodInput_ReturnsTrue()
        {
            //Arrange     
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            var actualAmount = ExchangeCalculator.AmountCalculator(100, 1.5m);
            var expectedAmount = 150m;

            //Assert        
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void AmountCalculator_ZeroInput_ReturnsZero()
        {
            //Arrange     
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            var actualAmount = ExchangeCalculator.AmountCalculator(0, 1.5m);
            var expectedAmount = 0;

            //Assert        
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        private void AmountCalculator_NegativeInput_ThrowsCustomException()
        {
            //Arrange     
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            CustomException exception = Assert.Throws<CustomException>(() => ExchangeCalculator.AmountCalculator(-100, 1.5m));

            //Assert
            Assert.Equal("The amount must be a positive number.", exception.Message);
        }

        [Fact]
        private void AmountCalculator_NegativeRate_ThrowsCustomException()
        {
            //Arrange     
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            CustomException exception = Assert.Throws<CustomException>(() => ExchangeCalculator.AmountCalculator(100, -1.5m));

            //Assert
            Assert.Equal("The rate must be a positive number.", exception.Message);
        }

        [Fact]
        private void AmountCalculator_ZeroRate_ThrowsCustomException()
        {
            //Arrange     
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            CustomException exception = Assert.Throws<CustomException>(() => ExchangeCalculator.AmountCalculator(100, 0));

            //Assert
            Assert.Equal("The rate must be a positive number.", exception.Message);
        }

        [Fact]
        private void RateCalculator_ZeroRate_ThrowsCustomException()
        {
            //Arrange
            FrozenDictionary<string, decimal> FrozenRates = dictionary.ToFrozenDictionary();
            var ExchangeCalculator = new ExchangeCalculator();

            //Act
            CustomException exception = Assert.Throws<CustomException>(() => ExchangeCalculator.RateCalculator("Tst", "Tst2", FrozenRates));

            //Assert
            Assert.Equal("The rates must be   positive numbers.", exception.Message);
        }
    }
}