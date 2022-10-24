﻿namespace Cookmate.Infrastructure
{
    using System.Security.Claims;
    using static Cookmate.Areas.Admin.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user) 
            => user.IsInRole(AdministratorRoleName);
    }
}
