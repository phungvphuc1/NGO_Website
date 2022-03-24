using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Programs")]
    public class Programs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ShortContent { get; set; }
        [Required]
        public string Content { get; set; }
        public double? ExpectedAmount { get; set; }
        public int? PartnerId { get; set; }
        public DateTime Duration { get; set; }
        public int? Status { get; set; }//1: đang quyên góp /2: đã quyên góp xong nhưng chưa thực hiện/ 3: đã làm(tiền đã trao tới đích)
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public virtual Partner GetPartner { get; set; }
        [ForeignKey("ProgramId")]
        public virtual IEnumerable<Interested> GetInteresteds { get; set; }
        public virtual IEnumerable<Photos> GetPhotos { get; set; }
        [ForeignKey("ProgramId")]
        public virtual IEnumerable<Donate> GetDonates { get; set; }
    }
}
