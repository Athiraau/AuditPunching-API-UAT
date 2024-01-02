using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class PhotoUpdateReqDto
    {
        public string p_indata { get; set; } = string.Empty;
        public string p_flag { get; set; } = string.Empty;

        public string empCode { get; set; } = string.Empty;
        // [Required]
        public byte[] empPhoto { get; set; }
    }
}
