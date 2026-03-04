using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<RolesUsers> AsignRoleByUserId(Guid userId, int roleId, CancellationToken cancellationToken);
        Task<RolesUsers> ModifyRoleByUserId(Guid userId, int oldRoleId, int newRoleId, CancellationToken cancellationToken);
    }
}
