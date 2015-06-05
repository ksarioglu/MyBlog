using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Post
    {
        public Post()
        {
            DateCreated = DateTime.Today;
        }


        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comments { get; set; }
     
    }
}