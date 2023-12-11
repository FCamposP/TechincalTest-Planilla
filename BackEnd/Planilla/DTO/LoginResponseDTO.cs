using Planilla.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class LoginResponseDTO
    {
        public TokenDTO TokenResponse { get; set; }
        public int UserId { get; set; }

        public LoginResponseDTO(TokenDTO tokenResponse, int userId)
        {
            TokenResponse = tokenResponse;
            UserId = userId;
        }
    }
}
