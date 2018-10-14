using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    /// <summary>
    /// Worker with fixed monthly vage
    /// </summary>
    class WorkerFixed : Worker
    {
        double monthlyVage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="age">int</param>
        /// <param name="hour">double</param>
        public WorkerFixed(string name, int age, double monthlyVage) : base(name, age)
        {
            this.monthlyVage = monthlyVage;
        }
        /// <summary>
        /// Method return monthly vage
        /// </summary>
        /// <returns></returns>
        public override double MonthPay()
        {
            return monthlyVage;
        }
    }

}
