using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Interfaces
{
    public class UI : IUIRepository
    {
        public decimal RequestDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result)) ;
            return result;
        }
    }
}
