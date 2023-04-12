using VinylTown.Extensions;
using VinylTown.Interfaces;

namespace VinylTown.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _accessor;
    public UserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public string GetUserId()
    {
        if (_accessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return _accessor.HttpContext.User.GetUserId();
        }
        else return string.Empty;
    }

}
