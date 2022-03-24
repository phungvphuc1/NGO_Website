using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models
{
    public class Email
    {
        public string Subject { get; set; }
        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Recipient { get; set; }
    }
}
