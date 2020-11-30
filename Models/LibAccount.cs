using System;
using System.Collections.Generic;

namespace SODV3201_LibMgtSys.Models
{
    public class LibAccount
    {
        public Guid ID { get; set; }
        public int ItemsCheckedOut { get; set; }
        public Person Owner { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; }
    }
}