using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.Entities
{
    class Stock
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public DateTime SupplyDate { get; set; }
        public float PurchasePrice { get; set; }
        public float SellingPrice { get; set; }
    }
}
