using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    /// <summary>
    /// Worker with hour rate payment
    /// </summary>
    class WorkerHourPay : Worker
    {
        double hourCost;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="age">int</param>
        /// <param name="hour">double</param>
        public WorkerHourPay(string name, int age, double hourCost) : base(name, age)
        {
            this.hourCost = hourCost;
        }
        /// <summary>
        /// Method counting monthly average vage.
        /// </summary>
        /// <returns></returns>
        public override double MonthPay()
        {
            return 20.8 * 8 * hourCost;
        }
    }
}
