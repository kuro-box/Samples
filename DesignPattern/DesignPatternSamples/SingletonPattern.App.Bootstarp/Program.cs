using System;

namespace SingletonPattern.App.Bootstarp
{
    /// <summary>
    /// 演示单例模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var i1 = Singleton.Instance;
            var i2 = Singleton.Instance;

            Console.WriteLine(i1.Equals(i2));

            Console.Read();
        }
    }

    /// <summary>
    /// 单例模式的实现
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// 定义私有的构造函数，禁止创建实例
        /// </summary>
        private Singleton() { }

#pragma warning disable
        private int testNumber = 0;
#pragma warning restore

        #region 实现方式1
        //// 定义一个标识确保线程同步
        //private static readonly object locker = new object();

        //// 定义一个静态变量来保存类的实例，volatile关键字用于确保线程安全
        //private static volatile Singleton instance = null;

        ///// <summary>
        ///// 创建实例，此处要确保线程安全
        ///// </summary>
        ///// <returns></returns>
        //private static Singleton InitializationInstance(int num = 999)
        //{
        //    // 这里的判断并非没有意义
        //    // 没有这个判断第二次进来的时候，需要先lock，再判断
        //    // 这个lock的意义，在与之后的访问不需要再进lock了
        //    if (instance == null)
        //    {
        //        lock (locker)
        //        {
        //            if (instance == null)
        //                instance = new Singleton { testNumber = num };
        //        }
        //    }

        //    return instance;
        //}

        ///// <summary>
        ///// 定义公共的属性来提供一个全局的访问入口，适用于无参数构建实例
        ///// </summary>
        //public static Singleton Instance
        //{
        //    get { return InitializationInstance(); }
        //}

        ///// <summary>
        ///// 定义公共的函数来提供一个全局的访问入口，适用于带参数构建实例
        ///// </summary>
        ///// <returns></returns>
        //public static Singleton GetInstance(int num)
        //{
        //    return InitializationInstance(num);
        //}
        #endregion

        #region 实现方式2
        // 定义公共的变量来提供一个全局的访问入口，适用于无参数构建实例
        public static readonly Singleton Instance = null;

        static Singleton()
        {
            Instance = new Singleton { testNumber = 999 };
        }
        #endregion
    }
}
