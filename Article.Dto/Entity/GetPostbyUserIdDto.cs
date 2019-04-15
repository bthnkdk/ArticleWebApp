using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class GetPostbyUserIdDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
        public bool IsActive { get; set; }
    }
}
