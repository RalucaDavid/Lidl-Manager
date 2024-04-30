using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.Entities
{
    class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Barcode { get; set; } 
        public DateTime ExpirationDate { get; set; }
        public string Category { get; set; }
        public IList<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
