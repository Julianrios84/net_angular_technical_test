namespace Service.Security.Api.Core.Security
{
    public class UserInSession : IUserInSession
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserInSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserInSession()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
            return userName;
        }
    }
}
