using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SODV3201_LibMgtSys.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [RegularExpression(@"^[a-zA-Z]+", ErrorMessage = "Name can only contain letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        [RegularExpression(@"^[a-zA-Z]+", ErrorMessage = "Name can only contain letters.")]
        public string FirstName { get; set; }

    }
}