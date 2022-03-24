using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Partner")]
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]

        [Display(Name = "Organization Name")]
        public string OrgName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0[3|5|7|8|9])+([0-9]{8})$",
        ErrorMessage = "Phone is required and must be properly formatted.")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name ="Category Type")]
        public int CategoryId { get; set; }
        public string Logo { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("CategoryId")]
        public virtual DonateCategory GetDonateCategory { get; set; }
 
        public virtual IEnumerable<Donate> GetDonates { get; set; }
        public virtual IEnumerable<Programs> GetPrograms { get; set; }
        public virtual IEnumerable<Interested> GetInteresteds { get; set; }
    }
}
