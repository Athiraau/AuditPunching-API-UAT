using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IAuditPunchRepo
    {
        public Task<dynamic> GetAuditPunchEmpData(string flag, string indata);
        public Task<dynamic> PostAuditPunching(PhotoUpdateReqDto punchPostReq);


    }
}
