using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class PostDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string PostImageUrl { get; set; }
        public string UserImageUrl { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }

    }
}
