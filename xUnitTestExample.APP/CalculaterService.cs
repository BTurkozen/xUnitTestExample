using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestExample.APP
{
    public class CalculaterService : ICalculaterService
    {
        public int Add(int firstNumber, int secondNumber)
        {
            if (firstNumber == 0 || secondNumber == 0) return 0;

            return firstNumber + secondNumber;
        }

        public int Multip(int firstNumber, int secondNumber)
        {
            if (firstNumber == 0 || secondNumber == 0)
            {
                throw new Exception("firstNumber veya secondNumber not  value equal 0");
            }

            return firstNumber * secondNumber;
        }
    }
}
