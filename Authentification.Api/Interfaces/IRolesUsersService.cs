using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRolesUsersService
    {
        Task<RolesUsers> InsertRolesUsers(RolesUsers rolesUsers, CancellationToken cancellationToken);
        Task<RolesUsers> AddDefault(Guid userId, CancellationToken cancellationToken);
        Task<RolesUsers> UpdateRolesUsers(RolesUsers rolesUsers, int newRoleId, CancellationToken cancellationToken);
        Task DeleteRolesUsers(RolesUsers roleUsers, CancellationToken cancellationToken);
        Task<List<Roles>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default);
    }
}
