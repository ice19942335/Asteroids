using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambdas_Fun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Fun with lambdas***");
            //TraditionalDelegateSyntax();
            //AnonymousMethodSyntax();
            LambdaExpressionSyntax();
            Console.ReadKey();
        }

        static void LambdaExpressionSyntax()
        {
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

            List<int> evenNumbers = list.FindAll(i => (i % 2) == 0);
            Console.WriteLine("Here are your even numbers:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.ReadKey();

        }


        static void AnonymousMethodSyntax()
        {
            // Создать список целых чисел
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
            // Теперь использовать анонимный метод
            List<int> evenNumbers = list.FindAll(delegate (int i)
                { return (i % 2) == 0; });
            // Вывод четных чисел
            Console.WriteLine("Here are your even numbers:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.WriteLine();
        }

        static void TraditionalDelegateSyntax()
        {
            // Создать список целых чисел
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 4, 4 });
            // Вызов FindAll() с использованием традиционного синтаксиса делегатов
            // Создаем обобщенный экземпляр обобщенного делегата, используя встроенный делегат Predicate
            Predicate<int> predicate = new Predicate<int>(IsEvenNumber);
            // Создаем список целых чисел, используя метод FindAll, в который передаем делегат
            List<int> evenNumbers = list.FindAll(predicate);
            Console.WriteLine("Здесь только четные числа:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.WriteLine();
        }
        // Цель для делегата Predicate<>.
        static bool IsEvenNumber(int i)
        {
            // Это четное число?
            return (i % 2) == 0;
        }
    }
}
