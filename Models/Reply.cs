using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Reply")]
    public class Reply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="FullName")]
        public int? AccountId { get; set; }
        [Required]
        public string Content { get; set; }
        //answer for queryId
        [Display(Name ="Title")]
        public int? QueryId { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account GetAccount { get; set; }
        [ForeignKey("QueryId")]
        public virtual Query GetQuery { get; set; }
    }
}
