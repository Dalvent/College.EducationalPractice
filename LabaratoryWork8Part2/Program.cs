using LabaratoryWork8Part2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork8Part2
{
    class Program
    {
        private static Book[] defualtFileBooks = new Book[]
        {
            new Book() { Сipher = "131445531234", Author = "Robert Knut", Name = "Pizdets v codinge 1", IssueDate = DateTime.Now.AddYears(-2), RackNumber = 1 },
            new Book() { Сipher = "235235235235", Author = "Robert Cecil Martin", Name = "Clean Code" , IssueDate = DateTime.Now.AddYears(-1), RackNumber = 2 },
            new Book() { Сipher = "523523532532", Author = "Martin Fowler", Name = "Refactoring" , IssueDate = DateTime.Now, RackNumber = 3 },
            new Book() { Сipher = "124214214122", Author = "Petzold Charles", Name = "Code: The Hidden Language of Computer Hardware and Software", IssueDate = DateTime.Now, RackNumber = 4 },
            new Book() { Сipher = "754785685345", Author = "Thomas H. Cormen", Name = "Introduction to Algorithms", IssueDate = DateTime.Now, RackNumber = 5 },
            new Book() { Сipher = "131445531234", Author = "Robert Knut", Name = "Pizdets v codinge 2", IssueDate = DateTime.Now.AddYears(-2), RackNumber = 6 },
        };
        private const string FILE_NAME = "books.txt";
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            if (!new FileInfo(FILE_NAME).Exists)
                Book.SaveToDisk(FILE_NAME, defualtFileBooks);

            //var stopWatch1 = new Stopwatch();
            //stopWatch1.Start();
             var books = Book.GetFromDisk(FILE_NAME);
            //stopWatch1.Stop();
            //Console.WriteLine(stopWatch1.ElapsedMilliseconds);

            //var stopWatch2 = new Stopwatch();
            //stopWatch2.Start();
            //var books2 = Book.GetFromDiskNonParallel(FILE_NAME);
            //stopWatch2.Stop();
            //Console.WriteLine(stopWatch2.ElapsedMilliseconds);

            Console.WriteLine("Вся коллекция");
            foreach (var book in books)
            {
                Console.WriteLine($"Местоположение - {book.RackNumber}, автор - {book.Author}, название - {book.Name}");
            }

            Console.WriteLine("Книги кнута в коллекции");
            foreach (var book in books.Where(item => item.Author == "Robert Knut"))
            {
                Console.WriteLine($"{book.Name}");
            }
            Console.WriteLine($"Кол-во книг {DateTime.Now.Year} года");
            Console.WriteLine(books.Where(item => item.IssueDate.Year == DateTime.Now.Year).Count());
            Console.ReadKey();
        }
    }
}
