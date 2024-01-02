using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class PunchPostReqDto
    {
        public string flag { get; set; } = string.Empty;
        public string indata { get; set; } = string.Empty;
        public string empCode { get; set; } = string.Empty;
        // [Required]
        public string empPhoto { get; set; } = string.Empty;
    }
}
