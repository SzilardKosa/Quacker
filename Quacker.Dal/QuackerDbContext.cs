using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Quacker.Dal.Entities;

namespace Quacker.Dal
{
    public class QuackerDbContext: DbContext
    {
        public QuackerDbContext() { }
        public QuackerDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// The Posts db table
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}
