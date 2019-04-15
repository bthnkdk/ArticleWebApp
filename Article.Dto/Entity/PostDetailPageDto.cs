using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class PostDetailPageDto
    {
        public PostDetailDto PostDetail { get; set; }
        public List<PostUserDto> PostList { get; set; }
        public CategoryDto Category { get; set; }
        public List<CommentUserDto> Comments { get; set; }

    }
}
