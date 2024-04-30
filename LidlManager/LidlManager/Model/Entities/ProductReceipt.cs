using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.Entities
{
    class ProductReceipt
    {
        public Guid Id { get; set; }
        public Producer Producer { get; set; }
        public Receipt Receipt { get; set; }
        public float Amount { get; set; }
        public float Subtotal { get; set; }

    }
}
