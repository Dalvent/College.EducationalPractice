using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork8Part2.Model
{
    public class Book
    {
        public string Сipher { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public DateTime IssueDate { get; set; }
        public int RackNumber { get; set; }

        private static IEnumerable<string> ParallelLineReader(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }


        public static IEnumerable<Book> GetFromDisk(string fileName)
        {
            using (var reader = new StreamReader(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read)))
            {
                // long - line number.
                var books = new ConcurrentBag<(long, Book)>();
                Parallel.ForEach(ParallelLineReader(reader), (line, _, lineNumber) => {
                    books.Add((lineNumber, LineToBook(line)));
                });
                return books.OrderBy(item => item.Item1).Select(item => item.Item2);
            }
        }

        public static IEnumerable<Book> GetFromDiskNonParallel(string fileName)
        {
            using (var reader = new StreamReader(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read)))
            {
                // long - line number.
                var result = new List<Book>();
                while (!reader.EndOfStream)
                {
                    result.Add(LineToBook(reader.ReadLine()));
                }
                return result;
            }
        }

        public static void SaveToDisk(string fileName, IEnumerable<Book> books) => File.WriteAllText(fileName, BooksToString(books));

        public static async Task SaveToDiskAsync(string fileName, IEnumerable<Book> books) => await Task.Run(() => SaveToDisk(fileName, books));
        public static async Task<IEnumerable<Book>> GetFromDiskAsync(string fileName) => await Task.Run(() => GetFromDisk(fileName));

        private static Book LineToBook(string line)
        {
            const int CIPHER_PROPERTY_INDEX = 0;
            const int AUTHOR_PROPERTY_INDEX = 1;
            const int NAME_PROPERTY_INDEX = 2;
            const int ISSUE_DATE_PROPERTY_INDEX = 3;
            const int ROCK_NUMBER_PROPERTY_INDEX = 4;

            var splittedLine = line.Split(';');
            var resultBook = new Book();
            resultBook.Сipher = splittedLine[CIPHER_PROPERTY_INDEX];
            resultBook.Author = splittedLine[AUTHOR_PROPERTY_INDEX];
            resultBook.Name = splittedLine[NAME_PROPERTY_INDEX];
            resultBook.IssueDate = splittedLine[ISSUE_DATE_PROPERTY_INDEX] != String.Empty ? Convert.ToDateTime(splittedLine[ISSUE_DATE_PROPERTY_INDEX]) : default(DateTime);
            resultBook.RackNumber = splittedLine[ROCK_NUMBER_PROPERTY_INDEX] != String.Empty ? Convert.ToInt32(splittedLine[ROCK_NUMBER_PROPERTY_INDEX]) : default(int);
            return resultBook;
        }

        private static string BooksToString(IEnumerable<Book> books)
        {
            var booksBuilder = new StringBuilder();
            using (var enumerator = books.GetEnumerator())
            {
                var haveNextElement = enumerator.MoveNext();
                while (haveNextElement)
                {
                    var current = enumerator.Current;
                    booksBuilder.Append(BookToLine(current));
                    haveNextElement = enumerator.MoveNext();
                    if (haveNextElement)
                    {
                        booksBuilder.Append("\n");
                    }
                }
            }

            return booksBuilder.ToString();
        }

        private static string BookToLine(Book book)
        {
            return $"{book.Сipher};{book.Author};{book.Name};{book.IssueDate};{book.RackNumber}";
        }
    }
}
