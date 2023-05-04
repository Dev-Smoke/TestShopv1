using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestShopv1.Services
{
    public static class GetHelper
    {
        public static string GetName(this ClaimsPrincipal user)
        {
            return user.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        }

        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
