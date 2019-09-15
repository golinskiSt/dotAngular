using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models
{
    public class UserModel: IdentityUser
    {
        [ForeignKey("ContactModel")]
        public ContactsModel Contacts { get; set; }
        public virtual ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
        public virtual ICollection<CommentsModel> Comments { get; set; } = new List<CommentsModel>();
        public DateTime DOB { get; set; }
        public string Descriptions { get; set; }
        public bool RememberMe { get; set; }
    }
}
