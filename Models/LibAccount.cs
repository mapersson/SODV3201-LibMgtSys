using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SODV3201_LibMgtSys.Models
{
    public class LibAccount
    {
        public Guid ID { get; set; }

        [Display(Name = "# of Loaned Books")]
        [Required(ErrorMessage = "Please enter a numeric value")]
        [RegularExpression(@"^[0-9]+")]
        public int ItemsCheckedOut { get; set; }
        public Person Owner { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; }
    }
}