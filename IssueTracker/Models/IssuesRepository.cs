using IssueTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Models
{
    class IssuesRepository : IIssuesRepository
    {
        IssueTrackerDbContext _context;
        public IssuesRepository(IssueTrackerDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCommentAsync(Comment comment)
        {
            _context.Add(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateIssueAsync(Issue issue)
        {
            _context.Add(issue);
            return await _context.SaveChangesAsync();
        }

        public async Task<Issue> GetIssueAsync(int id)
        {
            return await _context.Issues.FirstOrDefaultAsync(issue => issue.Id == id);
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync()
        {
            return await _context.Issues.ToListAsync();
        }

        public string GetIssueChangesSummary(Issue issue)
        {
            var sb = new StringBuilder();           
            foreach (var p in _context.Entry(issue).Properties)
            {
                if (p.IsModified)
                {
                    sb.Append($"{p.Metadata.Name} changed from {p.OriginalValue} to {p.CurrentValue}<br>");
                }
            }
            sb.Append("<br>");
            return sb.ToString();
                
        }

        public async Task<int> RemoveIssueAsync(Issue issue)
        {
            _context.Issues.Remove(issue);
            return await _context.SaveChangesAsync(); 
        }

        public async Task<int> UpdateIssueAsync(Issue issue)
        {
            _context.Issues.Update(issue);
            return await _context.SaveChangesAsync();
        }
    }
}
