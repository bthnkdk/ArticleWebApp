using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class CommentUserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
        public string Fullname { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Count { get; set; }

    }
}
