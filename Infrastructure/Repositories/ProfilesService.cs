using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class ProfilesService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory) : IProfilesService
    {
        public async Task<Profiles> GetProfileByUsername(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await (from p in context.Profiles join u in context.Users on p.UserId equals u.UserId where u.Username == username select p).FirstOrDefaultAsync(cancellationToken);
                return profile;
            }
        }

        public async Task<Profiles> InsertProfile(Profiles profiles, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                await context.Profiles.AddAsync(profiles, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return profiles;
            }
        }

        public async Task<Profiles> InsertProfilePictureByUsername(string usermane, string profilePicture, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Profiles profile = await GetProfileByUsername(usermane);
                profile.ProfilePicture = profilePicture;
                await context.SaveChangesAsync(cancellationToken);
                return profile;
            }
        }

        public async Task<Profiles> ModifyByRequest(Profiles profiles, ModifyProfileRequest request, CancellationToken cancellationToken = default)
        {
            profiles.Address = request.Address;
            profiles.FirstName = request.FirstName;
            profiles.LastName = request.LastName;
            profiles.PhoneNumber = request.PhoneNumber;
            return profiles;

        }

        public async Task<Profiles> ModifyProfileByUsername(string username, ModifyProfileRequest request, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Profiles profile = await GetProfileByUsername(username, cancellationToken);
                if (profile == null)
                {
                    throw new Exception("Profile not found!");
                }
                
                await ModifyByRequest(profile, request, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return profile;
                
            }
        }
    }
}
