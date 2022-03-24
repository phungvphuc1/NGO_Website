using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    [Table("Query")]
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Question")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name ="FullName")]
        public int? AccountId { get; set; }
        public int Status { get; set; } //1.Active 2.InActive/delete  default:1
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account GetAccount { get; set; }
        public virtual IEnumerable<Reply> GetReplies { get; set; }

    }
}
