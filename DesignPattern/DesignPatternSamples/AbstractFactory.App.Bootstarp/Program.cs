using System;

namespace AbstractFactory.App.Bootstarp
{
    /// <summary>
    /// 演示抽象工厂
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            new TextFactory().CreateWriter().Write();
            new TextFactory().CreateReader().Read();

            new DbFactory().CreateWriter().Write();
            new DbFactory().CreateReader().Read();

            new LogFactory().CreateWriter().Write();
            new LogFactory().CreateReader().Read();

            Console.Read();
        }
    }

    public abstract class Writer
    {
        public abstract void Write();
    }

    public class TextWriter : Writer
    {
        public override void Write()
        {
            Console.WriteLine("Write to Text");
        }
    }

    public class DbWriter : Writer
    {
        public override void Write()
        {
            Console.WriteLine("Write to Database");
        }
    }

    public class LogWriter : Writer
    {
        public override void Write()
        {
            Console.WriteLine("Write to Log");
        }
    }

    public abstract class Reader
    {
        public abstract void Read();
    }

    public class TextReader : Reader
    {
        public override void Read()
        {
            Console.WriteLine("Read content from Text");
        }
    }

    public class DbReader : Reader
    {
        public override void Read()
        {
            Console.WriteLine("Read content from Database");
        }
    }

    public class LogReader : Reader
    {
        public override void Read()
        {
            Console.WriteLine("Read content from Log");
        }
    }

    public abstract class BaseFactory
    {
        public abstract Writer CreateWriter();

        public abstract Reader CreateReader();
    }

    public class TextFactory : BaseFactory
    {
        public override Writer CreateWriter()
        {
            return new TextWriter();
        }

        public override Reader CreateReader()
        {
            return new TextReader();
        }
    }

    public class DbFactory : BaseFactory
    {
        public override Writer CreateWriter()
        {
            return new DbWriter();
        }

        public override Reader CreateReader()
        {
            return new DbReader();
        }
    }

    public class LogFactory : BaseFactory
    {
        public override Writer CreateWriter()
        {
            return new LogWriter();
        }

        public override Reader CreateReader()
        {
            return new LogReader();
        }
    }
}
