using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class ContactsModel
    {
        [Key]
        [ForeignKey("UserModel")]
        public UserModel ContactId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public ICollection<UserModel> Contacts { get; set; } = new List<UserModel>();


    }
}
