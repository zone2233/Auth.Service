using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public List<Users> Users { get; set; }
        public List<RolesUsers> RolesUsers { get; set; }
    }
}
