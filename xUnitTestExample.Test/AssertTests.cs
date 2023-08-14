namespace xUnitTestExample.Test
{
    public class AssertTests
    {
        #region Contains / DoesNotContain
        [Fact]
        public void ContainsTest()
        {
            var names = new List<string>()
            {
                "Burak",
                "Deneme",
                "Burada"
            };

            Assert.Contains(names, c => c == "Burak");
        }

        [Fact]
        public void DoesNotContainTest()
        {
            var names = new List<string>()
            {
                "Burak",
                "Deneme",
                "Burada"
            };

            Assert.DoesNotContain("Dayı", "Burak TÜRKÖZEN");
        }
        #endregion

        #region True / False
        [Fact]
        public void TrueTest()
        {
            //Assert.True(5 > 2);

            Assert.True("".GetType() == typeof(string));
        }

        [Fact]
        public void FalseTest()
        {
            Assert.False(5 < 2);
        }
        #endregion

        #region Macthes / DoesNotMatch
        [Fact]
        public void MacthesTest()
        {
            var regEx = "^dog";
            Assert.Matches(regEx, "dog is an animal");
        }

        [Fact]
        public void DoesNotMatchTest()
        {
            var regEx = "^dog";
            Assert.DoesNotMatch(regEx, "is dog an animal");
        }
        #endregion

        #region StartsWith / EndsWith
        [Fact]
        public void StartsWithTest()
        {
            Assert.StartsWith("bir", "bir masal");
        }

        [Fact]
        public void EndsWithTest()
        {
            Assert.EndsWith("bir", "masal bir");
        }
        #endregion

        #region Empty / NotEmpty
        [Fact]
        public void EmptyTest()
        {
            Assert.Empty(new List<string>());
        }

        [Fact]
        public void NotEmptyTest()
        {
            Assert.NotEmpty(new List<string>() { "deneme" });
        }
        #endregion

        #region InRange / NotInRange

        [Fact]
        public void AddInRangeTest()
        {
            Assert.InRange(10, 2, 20);
        }

        [Fact]
        public void AddNoInRange()
        {
            Assert.NotInRange(30, 2, 20);
        }

        #endregion

        #region Single

        [Fact]
        public void AddSingleTest()
        {
            Assert.Single<string>(new List<string>() { "denem" });
        }

        #endregion

        #region IsType / IsNotType

        [Fact]
        public void IsTypeTest()
        {
            Assert.IsType<string>("asdasd");
        }

        [Fact]
        public void IsNotTypeTest()
        {
            Assert.IsNotType<string>(123);
        }

        #endregion

        #region IsAssignableFrom

        [Fact]
        public void IsAssignableFromTest()
        {
            //Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>() { });

            Assert.IsAssignableFrom<Object>("");
        }

        #endregion

        #region Null / NotNull

        [Fact]
        public void NullTest()
        {
            string deger = null;

            Assert.Null(deger);
        }

        [Fact]
        public void NoNullTest()
        {
            string deger = "";

            Assert.NotNull(deger);
        }

        #endregion

        #region Equal / NotEqual

        [Fact]
        public void EqualTest()
        {
            int deger = 25;

            Assert.Equal<int>(25, deger);
        }

        [Fact]
        public void NotEqualTest()
        {
            int deger = 25;

            Assert.Equal<int>(26, deger);
        }
        #endregion
    }
}
