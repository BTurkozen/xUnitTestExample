using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestExample.APP
{
    public class Calculater
    {
        public int add(int firstNumber, int secondNumber)
        {
            if (firstNumber == 0 || secondNumber == 0) return 0;

            return firstNumber + secondNumber;
        }
    }
}
