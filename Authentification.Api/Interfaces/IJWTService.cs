using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        //public Task<string> GenerateLoginJWTToken(string username);
        //public Task<string> GenerateFirstAuthJWTToken(string username);
        public Task<string> GenerateAuthJWTToken(string username, AuthentificationState authentificationState);
    }
}
