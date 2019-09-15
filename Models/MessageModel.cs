using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class MessageModel
    {
        [Key]
        public string ID { get; set; }
        [ForeignKey("UserModel")]
        public UserModel SenderID { get; set; }
        public UserModel ReciverID { get; set; }
        public string Message { get; set; }
        public DateTime Date
        {
            get
            {
                return Date;
            }
            set
            {
                Date = DateTime.Now;
            }
        }

        public bool IsSeen { get; set; }
    }
}
