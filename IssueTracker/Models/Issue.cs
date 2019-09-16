using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public IssueStatus Status {get;set; }
        [Required]
        public string Theme { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual List<Comment> Comments {get;set;}

        internal void CopyFrom(Issue issue)
        {
            this.Theme = issue.Theme;
            this.Description = issue.Description;
            this.Status = issue.Status;
        }
    }
}
