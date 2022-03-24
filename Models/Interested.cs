using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Interested")]
    public class Interested
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? ProgramId { get; set; }
        public int? PartnerId { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public virtual Account GetAccount { get; set; }
        public virtual Programs GetPrograms { get; set; }
        public virtual Partner GetPartner { get; set; }
    }
}
