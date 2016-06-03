using System;

namespace SingletonPattern.App.Bootstarp
{
    class Program
    {
        static void Main(string[] args)
        {
            var i1 = SingleThreadSingleton.Instance1;
            var i2 = SingleThreadSingleton.Instance1;

            Console.WriteLine(i1.Equals(i2));

            Console.Read();
        }
    }

    /* 单件模式(Singleton Pattern)
     * 动机(Motivation)
     * 有些类，需要确保其在系统中只存在一个实例
     * 优点：简洁，易懂
     * 缺点：无法实现带参数的实例的创建
     */
    public class SingleThreadSingleton
    {
        private SingleThreadSingleton() { }//这里设置构造函数为私有，确保无法多次构造

        #region 实现方式1
        private static object lockObject = new object();//用于确保线程安全

        private static volatile SingleThreadSingleton instance1 = null;//不适用volatile关键字，依然可能发生线程不安全，这里的作用是DoubleCheck

        public static SingleThreadSingleton Instance1
        {
            get
            {
                lock (lockObject)
                {
                    if (instance1 == null)
                        instance1 = new SingleThreadSingleton();
                }

                return instance1;
            }
        }
        #endregion

        #region 实现方式2
        public static readonly SingleThreadSingleton instance2 = null;

        static SingleThreadSingleton()
        {
            instance2 = new SingleThreadSingleton();
        }
        #endregion
    }
}
