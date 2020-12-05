using System;
using System.ComponentModel.DataAnnotations;

namespace SODV3201_LibMgtSys.Models
{
    public class BookLoan
    {
        public Guid ID { get; set; }
        public Guid LibAccountID { get; set; }
        public Guid BookItemID { get; set; }

        [Display(Name = "Borrowed Date")]
        [DataType(DataType.Date)]
        [CurrentOrFutureDate]
        public DateTime BorrowedDate { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [CurrentOrFutureDate]
        public DateTime DueDate { get; set; }

        [Display(Name = "Returned Date")]
        [DataType(DataType.Date)]
        [CurrentOrFutureDate]
        public DateTime? ReturnedDate { get; set; }

        public BookItem BookItem { get; set; }
        [Display(Name = "Account")]
        public LibAccount LibAccount { get; set; }

        public BookLoan()
        {
            BorrowedDate = DateTime.Now;
            DueDate = DateTime.Now.Add(new TimeSpan(14, 0, 0, 0));
        }
    }

    public class CurrentOrFutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = Convert.ToDateTime(value);
            if (DateTime.Compare(date, DateTime.Today) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Borrowed date must be after todays date");
            }
        }
    }

}