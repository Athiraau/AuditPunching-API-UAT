using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class CommonDto
    {
        public string branch { get; set; } = string.Empty;

        public string p_flag { get; set; } = string.Empty;

        public string empCode { get; set; } = string.Empty;
        public int punch { get; set; } = 0;
    }
}
