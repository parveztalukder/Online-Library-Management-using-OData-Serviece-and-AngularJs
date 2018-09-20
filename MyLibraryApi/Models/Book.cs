using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLibraryApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookAuth { get; set; }              
        public int AvilableBook { get; set; }
        public int IssueBook { get; set; }

        [ForeignKey("Rack")]
        public int RackId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string BookStatus { get; set; }
        public virtual Category Category { get; set; }
        public virtual Rack Rack { get; set; }

       
    }
}