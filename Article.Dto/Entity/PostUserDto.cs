using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class PostUserDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PostImageUrl { get; set; }
        public string UserImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
        public int ViewCount { get; set; }

    }
}
