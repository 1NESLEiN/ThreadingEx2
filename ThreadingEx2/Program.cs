using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace FindSmallest
{
    class Program
    {

        private static readonly int[][] Data = new int[][]{
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

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
            Console.WriteLine("\t" + String.Join(", ", numbers) + "\n-> " + smallestSoFar);
            return smallestSoFar;
        }

        static void Main()
        {
            foreach (int[] d in Data)
            {
                Task findSmallestTask = Task.Run(() =>
                {
                    FindSmallest(d);
                });

                //To help keep the tasks from being killed, whenever the program stops
                Console.ReadLine();

                /*
                 * Using threads
                 * Now to use Tasks
                Thread t = new Thread(() =>
                {
                    int smallest = FindSmallest(d);
                    Console.WriteLine("\t" + String.Join(", ", d) + "\n-> " + smallest);
                });
                t.Start();
                 */
            }
        }
    }
}
