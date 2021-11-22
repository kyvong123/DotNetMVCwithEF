using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options):base(options)
        {

        }

        public DbSet<Item> DbItem { get; set; }
    }
}
