using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buisnessBudget
{
    internal class BudgetRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; } 
        public string RecordTime { get; set; }

        public BudgetRecord(string name, string type, decimal amount, string recordTime)
        {
            Name = name;
            Type = type;
            Amount = amount;
            RecordTime = recordTime;   
        }

    }

}
