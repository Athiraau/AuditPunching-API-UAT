using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IAuditPunchService
    {
        public Task<dynamic> GetPunchDataService(string flag, string indata);

        public Task<dynamic> PostPunchService(PunchPostReqDto _reqdto);
    }

}
