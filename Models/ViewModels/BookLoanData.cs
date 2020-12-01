using System.Collections.Generic;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Models.ViewModels
{
    public class BookLoanData
    {
        public IEnumerable<LibAccount> libAccounts { get; set; }
        public BookItem bookItem { get; set; }
        public BookLoan loan { get; set; }
    }
}