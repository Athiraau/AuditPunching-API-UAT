using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ServiceWrapper:IServiceWrapper
    {
        private readonly AuditPunchRepo _auditPunchRepo;  
        private IAuditPunchService _auditservice;
        private readonly DtoWrapper _dto;
        private IJwtUtils _jwtUtils;
        private readonly ILoggerService _logger;
        private IConfiguration _config;
        private readonly HelperWrapper _helper;

        public ServiceWrapper( AuditPunchRepo auditPunchRepo, IAuditPunchService service,DtoWrapper dto,
            IJwtUtils jwtUtils , ILoggerService logger, IConfiguration config, HelperWrapper helper)

        {
            _auditPunchRepo = auditPunchRepo;
            _auditservice = service;
            _dto = dto;
            _jwtUtils = jwtUtils;
            _logger = logger;
            _config = config;
            _helper = helper;
        }

        public IJwtUtils JwtUtils
        {
            get
            {
                if (_jwtUtils == null)
                {
                    _jwtUtils = new JwtUtils(_config,_logger);
                }
                return _jwtUtils;
            }
        }

        public IAuditPunchService auditPunchService
        {
            get
            {
                if (_auditservice == null)
                {
                   
                    _auditservice = new AuditPunchService(_auditPunchRepo, _dto,_config,_helper);
                }
                return _auditservice;
            }
        }

        


    }
}
