using System;

namespace GraphsLab
{
    class IO        //Класс реализует процессы ввода и вывода (input/output)
    {
        //IO fundamentals:
        public void PrintLine(string line, bool clearConsole)
        {
            Console.WriteLine(line);
            if (clearConsole)
                Console.Clear();
        }
        public void PrintLineWithKeyHolder(string line, bool clearConsole)
        {
            Console.WriteLine(line);
            Console.ReadKey();
            if (clearConsole)
                Console.Clear();
        }
        public int PrintLineWithIntegerReading(string line, bool clearConsole)
        {
            try
            {
                Console.WriteLine(line);
                if (clearConsole)
                    Console.Clear();
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                    throw new InvalidCastException();
            }
        }
        public double PrintLineWithDoubleReading(string line, bool clearConsole)
        {
            try
            {
                Console.WriteLine(line);
                if (clearConsole)
                    Console.Clear();
                return Convert.ToDouble(Console.ReadLine());
            }
            catch (Exception e)
            {
                throw new InvalidCastException();
            }
        }
        public string PrintLineWithStringReading(string line, bool clearConsole)
        {
            Console.WriteLine(line);
            if (clearConsole)
                Console.Clear();
            return Console.ReadLine();
        }
    }
}
