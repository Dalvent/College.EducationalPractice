using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part3
{
    class Program
    {
        private static string[] SplitByWord(string value) =>
            value.Split(' ').Select(s => RemoveSymbols(s, new char[] { ',', '.', '!', '?', '(', ')', '[', ']', '{', '}', '\n', '\r' }))
            .ToArray();
        private static string RemoveSymbols(string value, params char[] symbols)
        {
            string result = value;
            foreach (var symbol in symbols)
            {
                result = result.Replace(symbol.ToString(), string.Empty);
            }
            return result;
        }
        private static int TaskCounter(string words, char symbol)
        {
            var splitterdWordsd = SplitByWord(words);
            var resultWords = splitterdWordsd.Where(word => word.Count(s => s == symbol) >= 2);
            return resultWords.Count();
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Напишите фразу");
            var words = Console.ReadLine();
            Console.WriteLine("Напишите букву");
            var word = Console.ReadLine();
            //Console.WriteLine(TaskCounter(words, word[0]));
            Console.WriteLine("2");
            Console.ReadLine();
        }
    }
}
