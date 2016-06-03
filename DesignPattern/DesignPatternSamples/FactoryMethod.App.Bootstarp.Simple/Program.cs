using System;

namespace FactoryMethod.App.Bootstarp.Simple
{
    /// <summary>
    /// 演示简单工厂模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Writer writer = null;
            writer = WriterFactory.CreateWriter();
            writer.Write();
            writer = WriterFactory.CreateWriter(WriteMode.Database);
            writer.Write();
            writer = WriterFactory.CreateWriter(WriteMode.Log);
            writer.Write();

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

    public enum WriteMode
    {
        Text,
        Database,
        Log
    }

    public class WriterFactory
    {
        public static Writer CreateWriter(WriteMode mode = WriteMode.Text)
        {
            Writer writer = null;

            switch (mode)
            {
                case WriteMode.Database:
                    writer = new DbWriter();
                    break;
                case WriteMode.Log:
                    writer = new LogWriter();
                    break;
                case WriteMode.Text:
                default:
                    writer = new TextWriter();
                    break;
            }

            return writer;
        }
    }
}
