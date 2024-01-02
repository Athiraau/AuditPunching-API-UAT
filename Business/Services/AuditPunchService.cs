using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuditPunchService: IAuditPunchService
    {
        private readonly AuditPunchRepo _auditPunch;
        private readonly DtoWrapper _dto;
        private IConfiguration _config;
        private readonly HelperWrapper _helper;
        public AuditPunchService(AuditPunchRepo auditPunch,DtoWrapper dto, 
            IConfiguration config, HelperWrapper helper) 
        { 
            _auditPunch = auditPunch;
            _dto = dto;
            _config = config;
            _helper = helper;

            

        }


        public async Task<dynamic> GetPunchDataService(string flag, string indata)
        {
            var res = await _auditPunch.GetAuditPunchEmpData(flag, indata);
            _dto.punchPostResDto.AuditPunchData = res;

            return _dto.punchPostResDto;
        }

        public async Task<dynamic> PostPunchService(PunchPostReqDto _reqdto)
        {

            int compressSize = Convert.ToInt32(_config["Image:CompressionSize"]);

            PhotoUpdateReqDto docu_up = new PhotoUpdateReqDto();
            byte[] imageBytes = Convert.FromBase64String(_reqdto.empPhoto);

            imageBytes = _helper.PHelper.ReduceImageSize(imageBytes, compressSize);

            //ReduceImageSize(imageBytes, compressSize);

            docu_up.empPhoto = imageBytes;
           
            docu_up.p_flag = _reqdto.flag;
            docu_up.p_indata = _reqdto.indata;
            docu_up.empCode= _reqdto.empCode;

            var res = await _auditPunch.PostAuditPunching(docu_up);
            _dto.punchPostResDto.AuditPunchData = res;

            return _dto.punchPostResDto;
        }
    }
}
