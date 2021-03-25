using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new Queue<int>(Enumerable.Range(1, 5));
            var e = 3;
            RemoveByTask(queue, e);
            Console.WriteLine(string.Join(", ", queue));
            Console.ReadKey();
        }
        private static void RemoveByTask(Queue<int> queue, int e)
        {
            var eCountInQueue = queue.Count(item => item == e);
            for (int i = 0; i < eCountInQueue; i++)
            {
                var element = queue.Peek();
                if(element != e)
                    queue.Dequeue();
            }
        }
    }
}
