using FastEndpoints;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Requests
{
    public class ProfilePictureRequest
    {
        public IFormFile File { get; set; }
        public string Username  { get; set; }
    }
}
