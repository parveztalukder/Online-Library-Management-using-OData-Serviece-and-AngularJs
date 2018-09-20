using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLibraryApi.Models
{
    public class IssueBook
    {
       


        public int Id { get; set; }
        public string username { get; set; } 
        [Required]
        public DateTime IssueDate { get; set; }
        
        public DateTime DueDate { get; set; }
        //Navigation 
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}