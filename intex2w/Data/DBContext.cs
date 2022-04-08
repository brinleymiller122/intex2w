using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intex2w.Models;

namespace intex2w.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        //Crashes DB
        public DbSet<Crash> crashes { get; set; }
    }
}
