using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikePicsHw.Data
{
    public class PicDataContext : DbContext
    {
        private readonly string _connection;

        public PicDataContext(string connection)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        public DbSet<Image> Pictures { get; set; }
    }
}
