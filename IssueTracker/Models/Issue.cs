using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        [Required]
        public string Theme { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public Comment[] Comments {get;set;}
    }
}
