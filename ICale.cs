using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCastle
{
    public interface ICalc
    {
        int Add(int a, int b);
      
    }

    public class Calc : ICalc
    {
        public  int Add(int a, int b)
        {
            Console.WriteLine(string.Format("{0}+{1}={2}",a,b, a + b));
            return a + b;
        }
     
    }


    public class CalcInterceptor : IInterceptor
    {
        private int _max;
        public CalcInterceptor(int max)
        {
           this._max = max;
        }
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("进入拦截方法");
            int a = (int)invocation.GetArgumentValue(0);
            int b= (int)invocation.GetArgumentValue(1);

            if (a > this._max || b > this._max)
            {
                Console.WriteLine(string.Format("参数不能大于{0}", this._max));

                throw new ArgumentOutOfRangeException();
            }
            else
                invocation.Proceed();
        }
    }
}
