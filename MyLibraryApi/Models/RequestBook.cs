using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibraryApi.Models
{
    public class RequestBook
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string url { get; set; }
        public string Note { get; set; }

    }
}