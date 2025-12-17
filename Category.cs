using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal class Category
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public Category (string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
