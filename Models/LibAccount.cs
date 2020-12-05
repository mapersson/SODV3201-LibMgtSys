using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SODV3201_LibMgtSys.Models
{
    public class LibAccount
    {
        public Guid ID { get; set; }

        [Display(Name = "# of Loaned Books")]
        public int ItemsCheckedOut { get; set; }
        public Person Owner { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; }
    }
}