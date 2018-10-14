using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Тут ничего не сделанно и не доделанно

namespace Workers
{
    ///
    class Program
    {
        static void Main(string[] args)
        {
            List<Object> workers = new List<object>();

            for (int i = 0; i < 5; i++)
            {
                workers.Add(new WorkerFixed($"Worker number - {i + 1}", 20 + i, i * 5));
            }

            for (int i = 0; i < 5; i++)
            {
                workers.Add(new WorkerHourPay($"Worker number - {i + 1}", 20 + i, i * 30));
            }

            foreach (var item in workers)
            {
                Console.WriteLine();
            }






            Console.ReadKey();
        }
    }
}
