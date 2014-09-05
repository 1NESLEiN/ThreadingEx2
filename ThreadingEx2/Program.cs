using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace FindSmallest
{
    class Program
    {
        private static readonly int[][] Data = new int[][]
        {
            new[] {1, 5, 4, 2},
            new[] {3, 2, 4, 11, 4},
            new[] {33, 2, 3, -1, 10},
            new[] {3, 2, 8, 9, -1},
            new[] {1, 22, 1, 9, -3, 5}
        };

        private static List<int> _smallInts = new List<int>();

        private static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                }
            }
            return smallestSoFar;
        }

        static void Main()
        {
            List<Task<int>> taskList = new List<Task<int>>();

            foreach (int[] d in Data)
            {
                Task<int> findSmallestTask = new Task<int>(() =>
                {
                    int smallest = FindSmallest(d);
                    return smallest;
                });

                taskList.Add(findSmallestTask);
            }

            foreach (Task t in taskList)
            {
                t.Start();
            }

            // Not necessary, as task.Result awaits
            //Task.WaitAll(taskList.ToArray());

            foreach (Task<int> task in taskList)
            {
                Console.WriteLine(task.Result);
                _smallInts.Add(task.Result);
            }
            Console.WriteLine("\n" + "Smallest of all integers: " + FindSmallest(_smallInts.ToArray()));
        }
    }
}