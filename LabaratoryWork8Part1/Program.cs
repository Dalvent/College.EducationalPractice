using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork8Part1
{
    class Program
    {
        private static bool ShowFileInfo(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                WriteLineError("Файл не существует!");
                return false;
            }
            else
            {
                WriteLineInfo($"Вес файла: {fileInfo.Length / 1024} KB ({fileInfo.Length} bytes)");
                WriteLineInfo($"Аттриубты файла:");
                foreach (FileAttributes attribute in Enum.GetValues(typeof(FileAttributes)))
                {
                    if (fileInfo.Attributes.HasFlag(attribute))
                    {
                        WriteLineInfo($"{attribute}");
                    }
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Ведите имя файла");
            var fileName = Console.ReadLine();
            var isExist = ShowFileInfo(new FileInfo(fileName));

            if (isExist)
            {
                var startSymbol = 'а';
                var endSymbol = 'я';
                var symbols = Enumerable.Range((int)startSymbol, (int)(endSymbol - startSymbol + 1)).Select(i => (char)i).ToArray();
                var table = GetTableOfCountSymbolsInFile(fileName, symbols);
                WriteTableToFile("table.txt", table).GetAwaiter().GetResult();
            }

            Console.ReadLine();
        }

        private static async Task WriteTableToFile(string fileName, IEnumerable<(string, int)> table)
        {
            using (var fileWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8))
            {
                foreach (var row in table)
                {
                    await fileWriter.WriteAsync($"{row.Item1} {row.Item2}\n" );
                }
            }
        }

        private static IEnumerable<(string, int)> GetTableOfCountSymbolsInFile(string fileName, params char[] symbols)
        {
            var fileText = File.ReadAllText(fileName);
            var result = new ConcurrentBag<(string, int)>();
            Parallel.ForEach(symbols, symbol => {
                var textForParse = fileText.ToList();
                result.Add((symbol.ToString(), textForParse.Count(s => s.ToString().ToLower() == symbol.ToString())));
            });
            return result.OrderBy(item => item.Item1);
        }

        private static void WriteLineError(string error)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = currentColor;
        }

        private static void WriteLineInfo(string info)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(info);
            Console.ForegroundColor = currentColor;
        }
    }
}
