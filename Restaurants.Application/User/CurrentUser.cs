﻿namespace Restaurants.Application.User;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles,string? Nationality,DateOnly? DateOfBirth)
{
    public bool IsInRole(string RoleName) => Roles.Contains(RoleName);
}
