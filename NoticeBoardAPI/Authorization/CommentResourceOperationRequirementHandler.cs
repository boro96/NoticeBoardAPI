using Microsoft.AspNetCore.Authorization;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Authorization
{
    public class CommentResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Comment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement, Comment resource)
        {
            if(requirement.Operation == ResourceOperation.Create || requirement.Operation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = int.Parse(context.User.FindFirst(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if(resource.UserId == userId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
