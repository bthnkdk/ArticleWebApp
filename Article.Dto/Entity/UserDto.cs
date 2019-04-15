using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dto.Entity
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string WhichUpdate { get; set; }
        public byte[] Value { get; set; }
    }
}
