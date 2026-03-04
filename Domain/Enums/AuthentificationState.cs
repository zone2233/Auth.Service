using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum AuthentificationState : byte
    {
        QrAndSecretKey = 1,
        TOTPCode = 2,
        Checked = 3
    }
}
