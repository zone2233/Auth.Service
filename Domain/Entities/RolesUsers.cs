using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolesUsers
    {
        public int RolesRoleId { get; set; }
        public Guid UsersUserId { get; set; }
        public Users Users { get; set; }
        public Roles Roles { get; set; }
    }
}
