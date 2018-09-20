using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibraryApi.Models
{
    public class BookVM
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookAuth { get; set; }
        public int AvilableBook { get; set; }
        public int IssueBook { get; set; }
        public string Rack { get; set; }
        public string  Category { get; set; }
        public string BookStatus { get; set; }

     
    }
}