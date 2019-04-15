using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Core.Entities
{
    public class Comment : Base
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
