using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Fishes.Model
{
    public class FishesContext : DbContext
    {
        public FishesContext()
            : base("FishesDB")
        {
        }

        public DbSet<Page> Page { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}
