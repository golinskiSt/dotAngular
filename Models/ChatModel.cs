using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class ChatModel
    {
        [Key]
        public string ID { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
        public ICollection<UserModel> ChatUsers { get; set; }
        public string ChatName { get; set; }
    }
}
