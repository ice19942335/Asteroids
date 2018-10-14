using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Aleksejs Birula
 * а) Свернуть обращение к OrderBy с использованием лямбда - выражения $.
 * б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
 */

namespace Lesson_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Task_3a();
            //Task_2();
        }

        #region *Дан фрагмент программы:
        /// <summary>
        /// Третья задача A
        /// </summary>
        private static void Task_3a()
        {
            //а) Свернуть обращение к OrderBy с использованием лямбда - выражения $.

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two", 2 },
                {"one", 1 },
                {"three",3 },
            };

            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            /*Решение*/var d = dict.OrderBy(pair => pair.Value); //Я то сам сделал, но эта задача решается с решарпером в один клик

            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);

            Console.ReadKey();
        }
        #endregion *Дан фрагмент программы:

        #region Task_2

        private static void Task_2()
        {
            Console.WriteLine("How many times same integers is in the list\n");

            List<int> listInt = new List<int>() { 1, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 6, 6 };


            foreach (int num in listInt)
                Console.Write(num + " ");

            Console.WriteLine("\n");

            Dictionary<int, int> dictInt = new Dictionary<int, int>();
            foreach (int num in listInt)
            {
                int count = 0;

                if (!dictInt.ContainsKey(num))
                {
                    foreach (int num1 in listInt)
                    {
                        if (num == num1)
                            count++;
                    }
                    dictInt.Add(num, count);
                }
                else
                {
                    continue;
                }
                Console.WriteLine($"{num} this many times: {count}");
            }
        }

        #endregion

    }
}
