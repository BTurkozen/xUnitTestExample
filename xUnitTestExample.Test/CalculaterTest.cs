namespace xUnitTestExample.Test
{
    public class CalculaterTest
    {
        public Calculater Calculater { get; set; }

        public CalculaterTest()
        {
            this.Calculater = new Calculater();
        }

        [Fact]
        public void AddEqualFactTest()
        {
            // ----------------------------- Arrange -----------------------------

            // Değişkenleri initialize ettiğimiz yerdir.
            int numberFirst = 5, numberSecond = 10;

            // Yada nesne örneği oluşturacağımnız yerdir.
            //var calculater = new Calculater();

            // ----------------------------- Act -----------------------------

            // initialize ettiğimiz nesnelerimize parametreler verip test edeceğimiz methodları çalıştıracağımız yerdir.
            var total = Calculater.add(numberFirst, numberSecond);

            // ----------------------------- Assert -----------------------------

            // Act evresinden çıkan sonucun beklenen sonuç mu değil mi evresidir. Testin doğruluğunu kontrol ettiğimiz yerdir.
            Assert.Equal<int>(15, total);
            //Assert.NotEqual<int>(15, total);
        }

        [Theory, InlineData(2, 5, 7), InlineData(2, 8, 10)]
        public void AddEqualTheoryTest(int firstNumber, int secondNumber, int total)
        {
            // Arrange
            var calculater = new Calculater();

            // Act
            int actualTotal = calculater.add(firstNumber, secondNumber);

            // Assert
            Assert.Equal(total, actualTotal);
        }

        #region İsimlendirme Kurallına uygun şekilde revize edildi.

        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(2, 8, 10)]
        public void Add_SimpleValues_ReturnTotalValue(int firstNumber, int secondNumber, int expectedTotal)
        {
            var actualTotal = Calculater.add(firstNumber, secondNumber);

            Assert.Equal(expectedTotal, actualTotal);
        }

        [Theory]
        [InlineData(0, 5, 9)]
        [InlineData(2, 0, 10)]
        public void Add_ZeroValues_ReturnToZeroValue(int firstNumber, int secondNumber, int expectedTotal)
        {
            var actualTotal = Calculater.add(firstNumber, secondNumber);

            Assert.NotEqual(expectedTotal, actualTotal);
        }

        #endregion
    }
}
