using System;

namespace Prototype.App.Bootstarp
{
    /// <summary>
    /// 演示原型模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var b1 = new Book("b1");

            var b2 = b1.Clone() as Book;

            var b3 = b2.Clone() as Book;

            Console.WriteLine(b1.Id);
            Console.WriteLine(b2.Id);
            Console.WriteLine(b3.Id);

            Console.Read();
        }
    }

    public abstract class BaseBook
    {
        public string Id { get; set; }

        public BaseBook(string id = "")
        {
            Id = id;
        }

        public abstract BaseBook Clone();
    }

    public class Book : BaseBook
    {
        public Book(string id = "") : base(id) { }

        public override BaseBook Clone()
        {
            return MemberwiseClone() as BaseBook;
        }
    }
}
