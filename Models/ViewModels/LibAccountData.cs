using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Models.ViewModels
{
    public class LibAccountData
    {
        public Person Owner { get; set; }

        [Display(Name = "Book Loans")]
        public IEnumerable<BookLoan> BookLoans { get; set; }
    }
}