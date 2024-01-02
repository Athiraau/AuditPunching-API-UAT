using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repository
{
    public class AuditPunchRepo : IAuditPunchRepo
    {
        private DataContext _context; 
        private readonly DtoWrapper _dto;
        public AuditPunchRepo (DataContext context,DtoWrapper dto)
        {
            _context = context;
            _dto = dto;
           
        }

        public async Task<dynamic> GetAuditPunchEmpData(string flag, string indata)
        {
            OracleRefCursor result = null;
            var procedureName = "proc_audit_punch_data";
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", indata, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);


            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
            return response;
           

        }



        public async Task<dynamic> PostAuditPunching(PhotoUpdateReqDto punchPostReq)
        {
            OracleRefCursor result = null;

            var procedureName = "proc_audit_punch_data";
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", punchPostReq.p_flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", punchPostReq.p_indata, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);
            parameters.BindByName = true;
            using var connection = _context.CreateConnection();

        

            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            //---------PHOTO PUNCHING-----------------------//

            var query = " ";
            var data = punchPostReq.p_indata;

            string[] indata_text = data.Split('µ');

            _dto.commonDto.branch = indata_text[2];
            _dto.commonDto.punch= Convert.ToInt32(indata_text[1]);

                       
            if (_dto.commonDto.punch == 1)  //Arrival Photo punch
            {

                 query = " update DMS.hrm_audit_punch t set t.M_photo = :SBP  where t.emp_code = '" + punchPostReq.empCode + "'  and t.m_branch = '" + _dto.commonDto.branch + "' and t.curr_date = to_date(sysdate) and t.e_time is null and t.m_time is not null and t.m_photo is null";
            }
            else                             //Departure Photo punch
            {
                query = " update dms.hrm_audit_punch t set t.e_photo = :SBP  where t.emp_code = '" + punchPostReq.empCode + "'  and t.m_branch = '" + _dto.commonDto.branch + "' and t.curr_date = to_date(sysdate) and t.e_time is not null and t.m_time is not null and t.e_photo is null  and t.m_photo is not null";

            }

            connection.Open();
            OracleParameter[] prm = new OracleParameter[1];
            OracleCommand cmd = (OracleCommand)connection.CreateCommand();

            prm[0] = cmd.Parameters.Add("SBP", OracleDbType.Blob, punchPostReq.empPhoto, ParameterDirection.Input);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            return response;
            

        }


    }
}


