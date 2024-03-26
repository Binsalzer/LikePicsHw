using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikePicsHw.Data
{
    public class PicDataContextFactory:IDesignTimeDbContextFactory<PicDataContext>
    {
        public PicDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
              $"..{Path.DirectorySeparatorChar}LikePicsHw.Web"))
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new PicDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
