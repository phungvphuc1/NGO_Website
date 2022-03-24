using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="FullName is required!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required!"),DataType(DataType.EmailAddress),RegularExpression(@"^\S+@\S+$",ErrorMessage ="Email invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!"), DataType(DataType.Password),RegularExpression(@"^[\w\s]{8,12}$",ErrorMessage ="Password must from 8 to 12 character")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please Input Phone number!"), RegularExpression(@"^[0-9]{8,12}$", ErrorMessage = "invalid phone number")]
        public string Phone { get; set; }
      
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; } //default: images/avatar.jpg
        
        public int RoleId { get; set; }//1:Admin 2:User  //Default:2
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public virtual Roles GetRole { get; set; }
        public virtual IEnumerable<Donate> GetDonates { get; set; }
        public virtual IEnumerable<Interested> GetInteresteds { get; set; }
        public virtual IEnumerable<Query> GetQueries { get; set; }
        public virtual IEnumerable<Reply> GetReplies { get; set; }
    }
}
