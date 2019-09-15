using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class CommentsModel
    {
        [Key]
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        [ForeignKey("UserModel")]
        public UserModel CommenterID { get; set; }
        public string CommenterName { get; set; }
    }
}
