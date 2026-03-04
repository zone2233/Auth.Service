using Application.Features.Auth.Payloads.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfilesService
    {
        Task<Profiles> InsertProfile(Profiles profiles, CancellationToken cancellationToken = default);
        Task<Profiles> GetProfileByUsername(string username, CancellationToken cancellationToken = default);
        Task<Profiles> ModifyProfileByUsername(string username, ModifyProfileRequest request, CancellationToken cancellationToken = default);
        Task<Profiles> ModifyByRequest(Profiles profiles, ModifyProfileRequest request, CancellationToken cancellationToken = default);
        Task<Profiles> InsertProfilePictureByUsername(string usermane, string profilePicture, CancellationToken cancellationToken = default);
    }
}
