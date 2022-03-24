using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("ContactUs")]
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Input Phone number!"), RegularExpression(@"^[0-9]{10-12}$", ErrorMessage = "Phone invalid!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required!"), DataType(DataType.EmailAddress), RegularExpression(@"^\S+@\S+$", ErrorMessage = "Email invalid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please input content")]
        public string Content { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
    }
}
