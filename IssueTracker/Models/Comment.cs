using System;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public virtual Issue Issue {get;set; }
    }
}