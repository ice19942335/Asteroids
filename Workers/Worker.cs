using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    /// <summary>
    /// Abstract class Worker
    /// </summary>
    abstract class Worker
    {
        
        /// <summary>
        /// field name
        /// </summary>
        private string name;
        /// <summary>
        /// field age
        /// </summary>
        private int age;
        
        /// <summary>
        /// Get name
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return name;
        }
        /// <summary>
        /// Get age
        /// </summary>
        /// <returns></returns>
        public int Age()
        {
            return age;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">String</param>
        /// <param name="age">integer</param>
        public Worker(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Worker(){ }
        /// <summary>
        /// Abstract method for cout monthly average vage
        /// </summary>
        /// <returns></returns>
        public abstract double MonthPay();
    }
}
