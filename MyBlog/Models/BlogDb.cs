using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class BlogDb : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    
    }
}