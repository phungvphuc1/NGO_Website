using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Donate")]
    public class Donate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AccountId { get; set; }      
        public int? CategoryId { get; set; }
        public int? PartnerId { get; set; }
        public double Amount { get; set; }
        public int? ProgramId { get; set; }
        public int? Status { get; set; } //1:tiền vào quỷ nhưng chưa dc thực hiện  / 2:Tiền đã được dùng cho 1 program nào đó
        public DateTime CreatedAt { get; set; }
        public virtual Fund GetFund { get; set; }
        public virtual Account GetAccount { get; set; }
        public virtual DonateCategory GetDonateCategory { get; set; }
        public virtual Partner GetPartner { get; set; }
        public virtual Programs GetPrograms { get; set; }
    }
}
