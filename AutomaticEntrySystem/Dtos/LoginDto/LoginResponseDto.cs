namespace AutomaticEntrySystem.Dtos.LoginDto
{
    public class LoginResponseDto
    {
        public bool Status { get; set; }
        public string StatusMessage { get; set; }
        public string? AuthToken { get; set; }
    }
}
