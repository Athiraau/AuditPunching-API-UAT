using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditPunchAPI.Controllers
{
    
    // [Authorize]
    [AllowAnonymous]
    [Route("api/AuditPunch")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuditPunchController : Controller
    {
       // private readonly IAuditPunchService _service;
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly HelperWrapper _helper;



        public AuditPunchController(IServiceWrapper service, HelperWrapper helper, ILoggerService logger)
        {
            _service = service;
            _helper = helper;
            _logger = logger;
        }


        [HttpGet("GetAuditPunchEmpData/{flag}/{indata}", Name = "GetAuditPunchEmpData")]
        public async Task<IActionResult> GetAuditPunchEmpData([FromRoute] string flag, string indata)
        {
           var errorRes = _helper.CHelper.ValidateFlag(flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
          
            var punchdata = await _service.auditPunchService.GetPunchDataService(flag, indata);
           
            if (punchdata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
               // _logger.LogInfo($"Returned details of data required to load filter for flag: {flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }


        [HttpPost("PostAuditPunching", Name = "PostAuditPunching")]
        public async Task<IActionResult> PostAuditPunching([FromBody] PunchPostReqDto punchPostReq)
        {
            //FLAG VALIDATION
            var errorRes = _helper.CHelper.ValidateFlag(punchPostReq.flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }

            //IMAGE VALIDATION
            var imgRes = _helper.PHelper.ValidateImage(punchPostReq);


            //PUNCHING
            var punchdata = await _service.auditPunchService.PostPunchService(punchPostReq);
            if (punchdata == null)
            {
               _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
               _logger.LogInfo($"Returned response data after saving early going req: {punchPostReq.flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }


    }
}
