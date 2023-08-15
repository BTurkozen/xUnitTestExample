using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestExample.APP
{
    public class Calculater
    {
        private readonly ICalculaterService _calculaterService;

        public Calculater(ICalculaterService calculaterService)
        {
            _calculaterService = calculaterService;
        }

        public int add(int firstNumber, int secondNumber)
        {
            return _calculaterService.Add(firstNumber, secondNumber);
        }

        public int multip(int firstNumber, int secondNumber)
        {
            return _calculaterService.Multip(firstNumber, secondNumber);
        }
    }
}
