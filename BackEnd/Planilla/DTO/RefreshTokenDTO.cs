namespace Back.DT0
{
    public class RefreshTokenDTO
    {
        public string? Token { get; set; }
       

        public RefreshTokenDTO(string token)
        {
            Token = token;
        }
    }
}
