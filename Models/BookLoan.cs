using System;

namespace SODV3201_LibMgtSys.Models
{
    public class BookLoan
    {
        public Guid ID { get; set; }
        public Guid LibAccountID { get; set; }
        public Guid BookItemID { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public BookItem BookItem { get; set; }
        public LibAccount LibAccount { get; set; }

        public BookLoan()
        {
            BorrowedDate = DateTime.Now;
            DueDate = DateTime.Now.Add(new TimeSpan(14, 0, 0, 0));
        }
    }
}