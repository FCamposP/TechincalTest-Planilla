namespace Planilla.DTO.Auth
{
    public class TokenDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int? ExpireAt { get; set; }
    }
}
