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
        { "Tst1", 7m }
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

    }
}