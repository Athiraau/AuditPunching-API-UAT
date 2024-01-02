using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class CommonValidationHelper
    {
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<PunchPostReqDto> _PunchPostReqDtoValidator;

        public CommonValidationHelper(ErrorResponse error,DtoWrapper dto,IValidator<PunchPostReqDto> PunchPostReqDtoValidator)
        {
            _error = error;
            _dto = dto;
            _PunchPostReqDtoValidator = PunchPostReqDtoValidator;
                
        }

        public async Task<ErrorResponse> ValidateFlag(string flag)   //commonly using for all FLAG validation
        {
            ErrorResponse errorRes = null;

            _dto.punchPostReqDto.flag = flag;
            var validationResult = await _PunchPostReqDtoValidator.ValidateAsync(_dto.punchPostReqDto);

            errorRes = ReturnErrorRes(validationResult);

            return errorRes;
        }

       

        public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)

        {
            List<string> errors = new List<string>();
            foreach (var row in Res.Errors.ToArray())
            {
                errors.Add(row.ErrorMessage.ToString());
            }
            _error.errorMessage = errors;
            return _error;
        }



    }
}
