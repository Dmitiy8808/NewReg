using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Paging;

namespace Server.Authorization
{
    // public class IsRequestOwnerHandler :
    //     AuthorizationHandler<IsRequestOwnerRequirement, PagedList<RequestAbonent>>
    // {
    //     private readonly UserManager<User> _userManager;

    //     public IsRequestOwnerHandler(UserManager<User> userManager)
    //     {
    //         _userManager = userManager;
    //     }
    //     protected override async Task HandleRequirementAsync(
    //         AuthorizationHandlerContext context,
    //         IsRequestOwnerRequirement requirement,
    //         PagedList<RequestAbonent> resource)
    //     {
    //         var appUser = await _userManager.GetUserAsync(context.User);
    //         if (appUser == null)
    //         {
    //             return;
    //         }

    //         if (resource.MetaData. == appUser.Id)
    //         {
    //             context.Succeed(requirement);
    //         }
    //     }
    // }
}