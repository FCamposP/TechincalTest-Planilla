using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Utilities
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettingsJwt _appSettings;

        public JwtUtils(IOptions<AppSettingsJwt> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public int? ValidateToken(string token)
        {
            if (token == null || token == string.Empty)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            try
            {
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
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return -1;
            }
        }
    }
}
