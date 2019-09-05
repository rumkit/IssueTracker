using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;
using IssueTracker.Models;

namespace IssueTracker.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IIssuesRepository _issuesRepository;

        public CommentsController(IIssuesRepository issuesRepository)
        {
            _issuesRepository = issuesRepository;
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int issueId, string text)
        {
            var comment = new Comment()
            {
                Text = text,
                DateCreated = DateTime.Now,
                Issue = await _issuesRepository.GetIssueAsync(issueId)
            };            
            if(comment.Issue != null && !string.IsNullOrWhiteSpace(comment.Text))
            {
                await _issuesRepository.AddCommentAsync(comment);
            }            
            return RedirectToAction("Details", "Issues", new { id = issueId });
        }        
    }
}
