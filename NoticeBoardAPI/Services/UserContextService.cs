using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Services
{
    public interface IUserContextService
    {
        int GetUseId { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserContextService(IHttpContextAccessor httpContext)
        {
            _httpContextAccesor = httpContext;
        }

        public ClaimsPrincipal User => _httpContextAccesor.HttpContext?.User;
        public int GetUseId => int.Parse(User.FindFirst(a => a.Type == ClaimTypes.NameIdentifier).Value);
    }
}
