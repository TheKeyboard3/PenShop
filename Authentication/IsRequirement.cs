using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PenShop.Authentication;

public interface IIsRequirement : IAuthorizationRequirement{
    bool Matches(IdentityUser user);
}

public class IsRequirement<T> : IIsRequirement where T : class
{
    public IsRequirement(){
    }

    public bool Matches(IdentityUser user) => user is T;
}
