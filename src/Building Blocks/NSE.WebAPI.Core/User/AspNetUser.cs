using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSE.WebAPI.Core.User
{

    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return HasAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserEmail()
        {
            return HasAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public string GetUserToken()
        {
            return HasAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public bool HasAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool HasRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}