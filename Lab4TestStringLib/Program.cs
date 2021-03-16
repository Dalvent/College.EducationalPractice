using Lab4StringLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4TestStringLib
{
    class Program
    {
        private const string STRING_TO_REMOVE = "абвгдейййжжжззз";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Символы {STRING_TO_REMOVE}");
            Console.WriteLine($"Удаляем символы: {StringHelper.RemoveSymobols(STRING_TO_REMOVE, "агейз")}");
            Console.WriteLine($"Удаляем только повторяющиеся: {StringHelper.RemoveReapeatSymobols(STRING_TO_REMOVE, "агейз")}");
            Console.ReadLine();
        }
    }
}
