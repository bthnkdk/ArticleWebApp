using Article.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Interfaces
{
    public interface IComment
    {
        List<CommentUserDto> GetCommentAll(int postId);
        void Insert(CommentDto comment);

    }
}
