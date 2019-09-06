using IssueTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Data
{
    public class IssueTrackerDbContext : DbContext
    {
        public IssueTrackerDbContext(DbContextOptions<IssueTrackerDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Issue>()
            .Property(i => i.DateCreated)
            .HasDefaultValueSql("getdate()");
            builder.Entity<Comment>()
            .Property(c => c.DateCreated)
            .HasDefaultValueSql("getdate()");
            builder.Entity<Issue>()
            .Property(i => i.Status)
            .HasDefaultValue(IssueStatus.Open);
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
