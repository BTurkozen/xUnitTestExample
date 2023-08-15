using Moq;

namespace xUnitTestExample.Test
{
    public class CalculaterTest
    {
        public Calculater Calculater { get; set; }
        public Mock<ICalculaterService> CalculaterMock { get; set; }

        public CalculaterTest()
        {
            // Taklit edilecek Interface'i ayarlıyoruz.
            CalculaterMock = new Mock<ICalculaterService>();
            Calculater = new Calculater(CalculaterMock.Object);
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

            // Act
            int actualTotal = Calculater.add(firstNumber, secondNumber);

            // Assert
            Assert.Equal(total, actualTotal);
        }

        #region İsimlendirme Kurallına uygun şekilde revize edildi.

        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(2, 8, 10)]
        public void Add_SimpleValues_ReturnTotalValue(int firstNumber, int secondNumber, int expectedTotal)
        {

            // Kurulumu yapacağız. Burada davranış belirliyoruz.
            // Methodumuzu veriyoruz ve parametrelerini tanımlıyoruz.
            // Returns ile kabul edilmiş değerini giriyoruz. 
            CalculaterMock.Setup(s => s.Add(firstNumber, secondNumber)).Returns(expectedTotal);

            Assert.Equal(expectedTotal, Calculater.add(firstNumber, secondNumber));

            // Bir kere çalışma durumu Test ediliyor.
            CalculaterMock.Verify(v => v.Add(firstNumber, secondNumber), Times.Once);

            // Asla çalışmama durumunu Test eder.
            CalculaterMock.Verify(v => v.Add(firstNumber, secondNumber), Times.Never);

            // En az 2 kere çalıştığını Test eder.
            CalculaterMock.Verify(v => v.Add(firstNumber, secondNumber), Times.AtLeast(2));
        }

        [Theory]
        [InlineData(0, 5, 9)]
        [InlineData(2, 0, 10)]
        public void Add_ZeroValues_ReturnToZeroValue(int firstNumber, int secondNumber, int expectedTotal)
        {
            var actualTotal = Calculater.add(firstNumber, secondNumber);

            Assert.NotEqual(expectedTotal, actualTotal);
        }

        [Theory]
        [InlineData(0, 5, 9)]
        [InlineData(2, 0, 10)]
        public void AddMoqThrows_ZeroValues_ReturnToZeroValue(int firstNumber, int secondNumber, int expectedTotal)
        {
            CalculaterMock.Setup(s => s.Add(firstNumber, secondNumber)).Returns(expectedTotal);

            Assert.NotEqual(expectedTotal, Calculater.add(firstNumber, secondNumber));
        }

        #endregion

        [Theory]
        [InlineData(1, 0)]
        public void MultipThrows_ZeroValue_ReturnException(int firstNumber, int secondNumber)
        {
            var exceptionMessage = "firstNumber veya secondNumber not  value equal 0";

            CalculaterMock.Setup(s => s.Multip(firstNumber, secondNumber)).Throws(new Exception(exceptionMessage));

            Exception exception = Assert.Throws<Exception>(() => Calculater.multip(firstNumber, secondNumber));

            Assert.Equal(exceptionMessage, exception.Message);
        }
    }
}
