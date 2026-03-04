using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResetTokens
    {
        public int ResetTokensId { get; set; }
        public Guid UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime ReleaseTime { get; set; }
        public Users Users { get; set; }
    }
}
