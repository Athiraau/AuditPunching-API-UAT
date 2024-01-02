using DataAccess.Dto.Request;
using DataAccess.Dto;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contracts;

namespace Business.Helpers
{
    public class HelperWrapper:IHelperWrapper
    {

        private CommonValidationHelper _commonHelper;
        private PhotoValidationHelper _photoHelper;
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<PunchPostReqDto> _PunchPostReqDtoValidator;
        private readonly IValidator<PunchPostReqDto> _PhotoUpdateReqDtoValidator;

        public HelperWrapper(CommonValidationHelper commonHelper, PhotoValidationHelper photoHelper,
            ErrorResponse error,DtoWrapper dto, IValidator<PunchPostReqDto> PunchPostReqDtoValidator
            , IValidator<PunchPostReqDto> PhotoUpdateReqDtoValidator)


        {
            _commonHelper = commonHelper;
            _photoHelper = photoHelper;
            _error = error;
            _dto = dto;
            _PunchPostReqDtoValidator = PunchPostReqDtoValidator;
            _photoHelper= photoHelper;
            _PhotoUpdateReqDtoValidator = PhotoUpdateReqDtoValidator;
        }

        public CommonValidationHelper CHelper
        {
            get
            {
                if (_commonHelper == null)
                {
                    _commonHelper = new CommonValidationHelper(_error, _dto, _PunchPostReqDtoValidator);
                }
                return _commonHelper;
            }
        }


        public PhotoValidationHelper PHelper
        {
            get
            {
                if (_photoHelper == null)
                {
                    _photoHelper = new PhotoValidationHelper(_error, _dto, _PhotoUpdateReqDtoValidator);
                }
                return _photoHelper;
            }
        }





    }
}
