using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace WebApp1.Services
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationTokenAttribute : AuthorizeAttribute
    {
        public AuthorizationTokenAttribute()
        {

        }

        void IsUserAuthorized(string token)
        {

        }
    }
}
