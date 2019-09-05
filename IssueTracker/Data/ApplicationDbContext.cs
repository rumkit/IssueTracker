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

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
