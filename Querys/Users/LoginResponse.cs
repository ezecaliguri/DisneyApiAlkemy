namespace DisneyApi.Querys.Users
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }

        public string Information { get; set; }
    }
}
