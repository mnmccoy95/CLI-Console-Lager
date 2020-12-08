using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    public class BlogTag : Tag
    {
        public int BlogId { get; set; }

        public int TagId { get; set; }
    }
}
