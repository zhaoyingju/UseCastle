using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCastle
{

    //没有任何类实现该接口，通过动态代理调用接口，进而进入拦截器中
    public interface EmptyImpInterface
    {
        void Request(string str);
    }

    public class EmptyImpInterfaceInterceptor : IInterceptor
    {
       
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("你调了方法" + invocation.Method);
            Console.WriteLine("参数是" + invocation.GetArgumentValue(0));
        }
    }
}
