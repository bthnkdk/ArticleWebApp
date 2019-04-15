using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public byte[] Image { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedOnString { get; set; }
        public bool IsActive { get; set; }
    }
}
