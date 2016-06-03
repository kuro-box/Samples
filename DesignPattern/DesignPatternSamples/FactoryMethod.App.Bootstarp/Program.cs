using System;

namespace FactoryMethod.App.Bootstarp
{
    /// <summary>
    /// 演示标准工厂模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            new TextWriterFactory().CreateWriter().Write();
            new DbWriterFactory().CreateWriter().Write();
            new LogWriterFactory().CreateWriter().Write();

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

    public abstract class WriterFactory
    {
        public abstract Writer CreateWriter();
    }

    public class TextWriterFactory : WriterFactory
    {
        public override Writer CreateWriter()
        {
            return new TextWriter();
        }
    }

    public class DbWriterFactory : WriterFactory
    {
        public override Writer CreateWriter()
        {
            return new DbWriter();
        }
    }

    public class LogWriterFactory : WriterFactory
    {
        public override Writer CreateWriter()
        {
            return new LogWriter();
        }
    }
}
