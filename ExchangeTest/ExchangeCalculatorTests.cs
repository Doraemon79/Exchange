using System.Collections.Frozen;

namespace ExchangeTest
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
        public void AmountCalculator_GoodInput_ReturnsTrue()
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
        public void AmountCalculator_CurrencyNotIncluded_ReturnsTrue()
        {
            //Arrange
            FrozenDictionary<string, decimal> FrozenRates = dictionary.ToFrozenDictionary();
            var ExchangeCalculator = new ExchangeCalculator();


            //Act
            //   var actualResult = ExchangeCalculator.RateCalculator("Tst", "Tst4", FrozenRates);

            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(() => ExchangeCalculator.RateCalculator("Tst", "Tst4", FrozenRates));
            Assert.Equal("The currency 'Tst4' was not found in the dictionary.", exception.Message);
        }
    }
}