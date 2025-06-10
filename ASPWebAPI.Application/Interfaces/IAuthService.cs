namespace ASPWebAPI.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(string email, string password);
        Task<(string accessToken, string refreshToken)?> LoginAsync(string email, string password);
        Task<(string accessToken, string refreshToken)?> RefreshAsync(string refreshToken);
        Task<bool> RegisterAsync(string email, string password, string role);
        Task<bool> RevokeAsync(string refreshToken);
    }
}
