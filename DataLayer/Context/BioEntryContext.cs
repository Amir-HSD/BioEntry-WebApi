using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace DataLayer
{
    public class BioEntryContext : DbContext
    {
        public BioEntryContext() : base("data source=.;initial catalog=BioEntryDb;integrated security=True;")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Face> Faces { get; set; }
        public DbSet<Finger> Fingers { get; set; }
    }
}
