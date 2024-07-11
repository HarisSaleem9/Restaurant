﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }
        if (user.Identity == null || user.Identity.IsAuthenticated)
        {
            return null;
        }
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateofBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateofBirthString == null 
            ? (DateOnly?)null
            : DateOnly.ParseExact(dateofBirthString
            , "yyyy-MM-dd");
        return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
    }
}
