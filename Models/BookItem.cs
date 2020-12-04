using System;
using System.ComponentModel.DataAnnotations;

namespace SODV3201_LibMgtSys.Models
{
    public class BookItem
    {
        public Guid ID { get; set; }
        [RegularExpression(@"^[0-9]{10,13}", ErrorMessage = "Must be 10 or 13 digits")]
        [Required(ErrorMessage = "Please enter ISBN!")]
        public string ISBN { get; set; }
        [Display(Name = "Author Name")]
        [Required(ErrorMessage = "Please enter Author's Name!")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter Title of Book")]
        public string Title { get; set; }
    }
}