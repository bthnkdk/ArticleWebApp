namespace Article.Core.Entities
{
    public partial class User:Base
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public byte[] Image { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
