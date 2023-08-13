﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnitTestExample.APP;

namespace xUnitTestExample.Test
{
    public class CalculaterTest
    {
        [Fact]
        public void AddEqualTest()
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

        [Fact]
        public void AddContainTest()
        {
            var names = new List<string>()
            {
                "Burak",
                "Deneme",
                "Burada"
            };

            Assert.Contains(names, c => c == "Burak");

            //Assert.DoesNotContain("Dayı", "Burak TÜRKÖZEN");
        }

        [Fact]
        public void AddTrueorFalseTest()
        {
            //Assert.True(5 > 2);
            //Assert.False(5 < 2);

            Assert.True("".GetType() == typeof(string));
        }

        [Fact]
        public void AddMacthesTest()
        {
            var regEx = "^dog";
            //Assert.Matches(regEx, "dog is an animal");
            Assert.DoesNotMatch(regEx, "is dog an animal");
        }
    }
}
