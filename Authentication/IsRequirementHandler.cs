using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Authentication;

public class IsRequirementHandler : AuthorizationHandler<IIsRequirement>
{
    private PenShopContext _dbContext;

    public IsRequirementHandler(PenShopContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, IIsRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId is null)
            return Task.CompletedTask;

        var user = _dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == userId);
        if(user is null)
            return Task.CompletedTask;

        if (requirement.Matches(user))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
