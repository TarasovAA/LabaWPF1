using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLaba1.Models
{
    public class HeroesContext:DbContext
    {
        public HeroesContext() : base("DefaultConnection")
        {

        }
        public DbSet<Hero> Heroes { get; set; }
    }
}
