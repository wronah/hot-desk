namespace HotDesk.Api.Services.UserResolver
{
    public interface IUserResolver
    {
        int Id { get; }
        List<string> Roles { get; }
    }
}
