using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services;
using Business.Contracts;
using System.Configuration.Internal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;

        public JwtUtils(IConfiguration configuration, ILoggerService loggerservice)
        {
            _configuration = configuration;
           _logger = loggerservice;
        }

        public int? ValidateToken(string token)
        {
            if (token == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userID = int.Parse(jwtToken.Claims.First(x => x.Type == "empCode").Value);

            return userID;



        }
    }
}
