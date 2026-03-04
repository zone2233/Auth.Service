using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public class Settings
    {
        public IDictionary<string, string> ConnStrings { get; set; }
        public IDictionary<string, string> JWTSettings { get; set; }
        public string SendGridApiKey { get; set; }
    }
}
