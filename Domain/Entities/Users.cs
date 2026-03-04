using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public int PasswordTrys { get; set; }
        public DateTime? LastPasswordChange {  get; set; }
        public string? TOTPKey { get; set; }
        public Profiles Profiles { get; set; }
        //public List<Roles> Roles { get; set; }
        public List<RolesUsers> RolesUsers { get; set; }
        public List<ResetTokens> ResetTokens { get; set; }
    }
}
