using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Models
{
    public interface IIssuesRepository
    {
        Task<IEnumerable<Issue>> GetIssuesAsync();
        Task<Issue> GetIssueAsync(int id);
        Task<int> CreateIssueAsync(Issue issue);
        Task<int> RemoveIssueAsync(Issue issue);
        Task<int> UpdateIssueAsync(Issue issue);
        Task<int> AddCommentAsync(Comment comment);
        
    }
}
