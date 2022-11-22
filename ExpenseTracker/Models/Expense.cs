using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Models
{
    internal enum Category
    {
        Food,
        Housing,
        Travel,
        Clothing,
        PetCare,
        Miscellaneous
    }

    internal class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public string FileName { get; set; }
    }
}
