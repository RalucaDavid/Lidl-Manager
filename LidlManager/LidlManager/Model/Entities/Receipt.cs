using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.Entities
{
    class Receipt
    {
        public Guid Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float TotalSum { get; set; }
    }
}
