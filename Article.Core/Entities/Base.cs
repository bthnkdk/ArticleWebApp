using System.ComponentModel.DataAnnotations;

namespace Article.Core.Entities
{
    public abstract class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
