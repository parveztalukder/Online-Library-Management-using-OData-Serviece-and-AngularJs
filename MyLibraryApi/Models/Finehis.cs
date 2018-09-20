using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibraryApi.Models
{
    public class Finehis
    {
        public int Id { get; set; }
        public string username { get; set; }

        public int BookId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }
        public int amount { get; set; }
    }
}