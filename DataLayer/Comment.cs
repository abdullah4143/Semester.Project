using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
	public class Comment
	{
		public int comment_id { get; set; }
		public int blog_id { get; set; }
		public int user_id { get; set; }
		public string? content { get; set; }
		public string? publish_date { get; set; }
	}
}
