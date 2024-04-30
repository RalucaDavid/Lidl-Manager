using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LidlManager.Model
{
    class _Entities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,52916;Database=LidlManager;User id=user;Password=1234!Secret;TrustServerCertificate=true;");
        }
    }
}
