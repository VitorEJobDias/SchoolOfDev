using System.ComponentModel.DataAnnotations.Schema;
using SchoolOfDevs.Enuns;

namespace SchoolOfDevs.Entities
{
    public class User : BaseEntity
    {
        public int Age { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public TypeUser TypeUser { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string CurrentPassword { get; set; }
    }
}
