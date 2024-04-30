using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.Entities
{
    class Producer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public IList<Producer> Producers { get; set; } = new List<Producer>();
    }
}
