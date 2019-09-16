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

        public async Task<string[]> GetIssueChangesSummary(Issue issue)
        {
            var originalIssue = await _context.Issues.AsNoTracking().FirstAsync(i => i.Id == issue.Id);            
            var changeList = new List<string>();

            if(originalIssue.Theme != issue.Theme)
                changeList.Add($"Theme changed from {originalIssue.Theme} to {issue.Theme}");

            if (originalIssue.Description != issue.Description)
                changeList.Add($"Description changed from {originalIssue.Description} to {issue.Description}");

            if (originalIssue.Status != issue.Status)
                changeList.Add($"Status changed from {originalIssue.Status} to {issue.Status}");
            
            return changeList.ToArray();                            
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
