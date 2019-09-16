using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;
using IssueTracker.Models;
using System.Text;
using IssueTracker.Helpers;

namespace IssueTracker.Controllers
{
    public class IssuesController : Controller
    {
        IIssuesRepository _issuesRepository;

        public IssuesController(IIssuesRepository issuesRepository)
        {
            _issuesRepository = issuesRepository;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            return View(await _issuesRepository.GetIssuesAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var issue = await _issuesRepository.GetIssueAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Theme,Description,DateCreated")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                issue.DateCreated = DateTime.Now;
                await _issuesRepository.CreateIssueAsync(issue);
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var issue = await _issuesRepository.GetIssueAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string comment, [Bind("Id,Theme,Description,DateCreated,Status")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   

                    var changeCommnent = new Comment()
                    {
                        Text = HtmlFormatterHelper.MakeChangeList(await _issuesRepository.GetIssueChangesSummary(issue)) + comment,
                        Issue = issue
                    };                                        
                    await _issuesRepository.UpdateIssueAsync(issue);
                    await _issuesRepository.AddCommentAsync(changeCommnent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details),new { id = issue.Id});
            }
            return View(issue);
        }

        

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _issuesRepository.GetIssueAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _issuesRepository.GetIssueAsync(id);
            await _issuesRepository.RemoveIssueAsync(issue);
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _issuesRepository.GetIssueAsync(id).Result != null;
        }
    }
}
