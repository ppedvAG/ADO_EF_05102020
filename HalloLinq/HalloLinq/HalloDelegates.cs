using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloLinq
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitParameter(string text);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegates
    {
        public HalloDelegates()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAlsActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitParameter deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAction = MethodeMitPara;
            Action<string> deleMitParaActionAno = (string txt) => { Console.WriteLine(txt); };
            Action<string> deleMitParaActionAno2 = (txt) => Console.WriteLine(txt);
            Action<string> deleMitParaActionAno3 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Multi;
            long result = calcDele.Invoke(12, 56);
            Func<int, int, long> calcFunc = Sum;

            CalcDelegate calcDeleAno = (int x, int y) => { return x + y; };
            CalcDelegate calcDeleAno2 = (x, y) => { return x + y; };
            CalcDelegate calcDeleAno3 = (x, y) => x + y;

            var m = new List<Mitarbeiter>();
            m.Where(x => x.Name == "Fred");
            m.Where(Filter);
        }

        private bool Filter(Mitarbeiter arg)
        {
            if (arg.Name == "Fred")
                return true;
            else
                return false;
        }

        private long Multi(int a, int b)
        {
            return a * b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        public void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }

        public void MethodeMitPara(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
