using Newtonsoft.Json;
using System;

namespace Builder.App.Bootstarp
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager();

            var b1 = new ChineseWriter();
            manager.Construct(b1);
            b1.GetBook().Print();

            var b2 = new EnglishWriter();
            manager.Construct(b2);
            b2.GetBook().Print();

            Console.Read();
        }
    }

    public class Book
    {
        public string Title { get; set; }

        public decimal Amount { get; set; }

        public void Print()
        {
            Console.WriteLine(JsonConvert.SerializeObject(this));
        }
    }

    public abstract class Writer
    {
        public abstract void Write();

        public abstract Book GetBook();
    }

    public class ChineseWriter : Writer
    {
        private Book book = null;

        public override void Write()
        {
            book = new Book
            {
                Title = "中文书",
                Amount = 100.00M
            };
        }

        public override Book GetBook()
        {
            return book;
        }
    }

    public class EnglishWriter : Writer
    {
        private Book book = null;

        public override void Write()
        {
            book = new Book
            {
                Title = "English Book",
                Amount = 200.00M
            };
        }

        public override Book GetBook()
        {
            return book;
        }
    }

    public class Manager
    {
        public void Construct(Writer writer)
        {
            writer.Write();
        }
    }
}
