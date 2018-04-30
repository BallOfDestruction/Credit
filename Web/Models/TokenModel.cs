namespace Web.Models
{
    public class TokenModel : Entity<TokenModel>
    {
        public string Token { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
