using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Fund")]
    public class Fund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double? Total { get; set; }
        public double? CurrentFund { get; set; }
        public int? DonateId { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public virtual Donate GetDonate { get; set; }
    }
}
