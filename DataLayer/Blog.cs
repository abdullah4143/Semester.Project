using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Blog
    {
        public int blog_id { get; set; }
        public int user_id { get; set; }
        public int catagory_id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; } = string.Empty;
        public string? publish_date { get; set; }

    
    }
}
