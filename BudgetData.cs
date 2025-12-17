using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal class BudgetData
    {
        public int Total { get; set; }

        public string Currency {  get; set; }

        public List<Category> Categories { get; set; } = new();


    }
}
