using System;

namespace SODV3201_LibMgtSys.Models
{
    public class BookLoan
    {
        public Guid ID { get; set; }
        public Guid LibAcccountID { get; set; }
        public Guid BookItemID { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public BookItem BookItem { get; set; }
        public LibAccount LibAccount { get; set; }
    }
}