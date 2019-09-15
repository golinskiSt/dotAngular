using System.ComponentModel.DataAnnotations;

namespace WebApp1.Data
{
    internal class TUserToken
    {
        [Key]
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
    }
}