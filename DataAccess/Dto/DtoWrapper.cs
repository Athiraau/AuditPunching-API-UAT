using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        public PunchPostReqDto _punchPostReq;
        public PhotoUpdateReqDto _photoUpdateReq;
        public PunchPostResDto _punchPostRes;
        public CommonDto _commonDto;


        public PhotoUpdateReqDto photoUpdateReq
        {
            get
            {
                if (_photoUpdateReq == null)
                {
                    _photoUpdateReq = new PhotoUpdateReqDto();
                }
                return _photoUpdateReq;
            }
        }
        public CommonDto commonDto
        {
            get
            {
                if (_commonDto == null)
                {
                    _commonDto = new CommonDto();
                }
                return _commonDto;
            }
        }
        public PunchPostReqDto punchPostReqDto
        {
            get
            {
                if (_punchPostReq == null)
                {
                    _punchPostReq = new PunchPostReqDto();
                }
                return _punchPostReq;
            }
        }


        public PunchPostResDto punchPostResDto
        {
            get
            {
                if (_punchPostRes == null)
                {
                    _punchPostRes = new PunchPostResDto();
                }
                return _punchPostRes;
            }
        }


    }
}
