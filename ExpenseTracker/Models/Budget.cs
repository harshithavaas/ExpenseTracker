using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Models
{
    internal class Budget
    {
        
            public decimal budgetAmount { get; set; }
            public DateTime Date { get; set; }
            public string FileName { get; set; }
        
    }
}
