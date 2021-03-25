using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork8Part3
{
    class Program
    {

        private static void ShowFileInfo(FileInfo fileInfo)
        {
            Console.WriteLine($"{fileInfo.FullName} {fileInfo.Length / 1024} KB ({fileInfo.Length}) bytes");
        }

        private const int ARGUMENT_STRING_TO_FIND = 0;
        private const int ARGUMENT_DIRECTORY_PATH = 1;
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No arguments");
                return;
            }

            ShowAllFilesInfo(args[ARGUMENT_DIRECTORY_PATH], args[ARGUMENT_STRING_TO_FIND]);
            Console.ReadKey();
        }


        private static void ShowAllFilesInfo(string directoryPath, string value)
        {
            var files = GetAllFilesInDerictorWithChild(directoryPath);
            Parallel.ForEach(files, filePath => {
                try
                {
                    if (DomainExists(value, filePath))
                    {
                        ShowFileInfo(new FileInfo(filePath));
                    }
                }
                catch (Exception)
                {
                }
            });
        }

        private static bool DomainExists(string domain, string path)
        {
            foreach (string line in File.ReadLines(path))
                if (line.Contains(domain))
                    return true; // and stop reading lines

            return false;
        }

        private static IEnumerable<string> GetAllFilesInDerictorWithChild(string directoryPath)
        {
            foreach (var directory in GetAllDirectoriesPathes(directoryPath))
            {
                foreach (var file in GetAllFilesInDerictor(directory))
                {
                    yield return file;
                }
            }
        }

        private static IEnumerable<string> GetAllFilesInDerictor(string directoryPath)
        {
            var filesInDirectory = Directory.GetFiles(directoryPath);
            foreach (var item in filesInDirectory)
            {
                yield return item;
            }
        }

        private static IEnumerable<string> GetAllDirectoriesPathes(string directoryPath)
        {
            yield return directoryPath;
            var dictionariesInCurrentDirectories = Directory.GetDirectories(directoryPath);
            foreach (var directoryInCurrentDirectory in dictionariesInCurrentDirectories)
            {
                foreach (var item in GetAllDirectoriesPathes(directoryInCurrentDirectory))
                {
                    yield return item;
                }
            }
        }
    }
}
