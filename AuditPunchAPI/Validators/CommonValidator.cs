using DataAccess.Dto.Request;
using FluentValidation;

namespace AuditPunchAPI.Validators
{
    public class CommonValidator:AbstractValidator<PunchPostReqDto>
    {
        public CommonValidator() 
        {
            RuleFor(d => d.flag).NotNull().NotEmpty().WithMessage("Flag is required");


        }
    }
}
