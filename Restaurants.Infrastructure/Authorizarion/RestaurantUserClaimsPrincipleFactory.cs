﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorizarion;

public class RestaurantUserClaimsPrincipleFactory : UserClaimsPrincipalFactory<User,IdentityRole>
{

    public RestaurantUserClaimsPrincipleFactory(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager,roleManager,options){}
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        if (user.DateOfBirth !=null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }
        if (user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality!));
        }
        return new ClaimsPrincipal(id);
    }
}
