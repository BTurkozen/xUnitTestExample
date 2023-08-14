namespace xUnitTestExample.Test
{
    public class CalculaterTest
    {
        [Fact]
        public void AddEqualFactTest()
        {
            // ----------------------------- Arrange -----------------------------

            // Değişkenleri initialize ettiğimiz yerdir.
            int numberFirst = 5, numberSecond = 10;

            // Yada nesne örneği oluşturacağımnız yerdir.
            var calculater = new Calculater();

            // ----------------------------- Act -----------------------------

            // initialize ettiğimiz nesnelerimize parametreler verip test edeceğimiz methodları çalıştıracağımız yerdir.
            var total = calculater.add(numberFirst, numberSecond);

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
    }
}
