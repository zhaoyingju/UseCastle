using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCastle
{


    /// <summary>
    /// 
    /// CreateClassProxy<TClass>() 创建实现了泛型类的代理类，要求要代理的目标类的函数是虚函数
    /// CreateInterfaceProxyWithTarget<Tinterface>创建实现了泛型接口的代理类，把目标作为他的构造函数，调用代理类的方法，先进入拦截方法后进入目标方法。
    /// CreateInterfaceProxyWithoutTarget 创建实现了泛型接口的代理类，但代理类中的方法是空的，只调用了拦截方法
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1、正常的调用
            ICalc c = new Calc();
            c.Add(10, 10);

           
            var proxyGenerator = new ProxyGenerator();


            //2、创建Calc的代理类，调用个Add方法不会进入拦截方法中，因为Calc的add方法不是虚方法
            c = proxyGenerator.CreateClassProxy<Calc>(new CalcInterceptor(9));            
            c.Add(10, 10);

            //3、创建指定接口ICalc的代理类，但真正的实现在target=Calc中，改代理类实现了ICalc接口，内部包装了Calc，调用Add方法会进入拦截方法中
            c = proxyGenerator.CreateInterfaceProxyWithTarget<ICalc>(new Calc(),new CalcInterceptor(9));
            try
            {
                c.Add(10, 10);            
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Console.WriteLine("参数不合法");
            }


            //4、创建实现了EmptyImpInterface的代理类，没有指定目标，调用Request方法会进入拦截方法中
            EmptyImpInterface inter = proxyGenerator.CreateInterfaceProxyWithoutTarget<EmptyImpInterface>( new EmptyImpInterfaceInterceptor());
            inter.Request("hello world");

            Console.ReadKey();
            
        }
    }
}
