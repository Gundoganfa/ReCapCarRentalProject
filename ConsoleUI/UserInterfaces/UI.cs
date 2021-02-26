using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.UserInterfaces
{
    public class UI : IUIRepository
    {
        public decimal RequestDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result)) ;
            return result;
        }

        public decimal CountDays(DateTime dateOld, DateTime dateNew)
        {
            return (decimal)dateOld.Subtract(dateNew).TotalDays;
        }
    }
}
