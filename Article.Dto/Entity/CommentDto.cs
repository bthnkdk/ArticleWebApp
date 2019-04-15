using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
