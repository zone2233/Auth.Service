using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profiles
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? ProfilePicture {  get; set; }
        public Users Users { get; set; }

        public const string nameRegex = "^[a-zA-Z]+$";

        public const string phoneRegex = "^(?:\\+?40|0)(7[0-8]\\d{7})$";

    }
}
