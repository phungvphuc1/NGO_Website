using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models.ViewModels
{
    public class QuestionVM
    {
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Query> Queries { get; set; }
        public IEnumerable<Reply> Replies { get; set; }

        public IEnumerable<Query> OnePageQueries { get; set; }
       public Query query { get; set; }
        public Reply reply { get; set; }



    }
}
