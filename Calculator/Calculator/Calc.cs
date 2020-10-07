using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Calculator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {

            return checked(a + b);
        }

        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
