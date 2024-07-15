using System.Security.Claims;


namespace HotDesk.Api.Services.UserResolver
{
    public class UserResolver : IUserResolver
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        public UserResolver(IHttpContextAccessor httpContextAccessor)
        {
            if(httpContextAccessor.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }
            claimsPrincipal = httpContextAccessor.HttpContext.User;
        }

        public int Id => Int32.Parse(claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException(nameof(Id)));
        public List<string> Roles => claimsPrincipal?.FindFirst("roles")?.Value.Split(';').ToList() ?? new List<string>();
    }
}
