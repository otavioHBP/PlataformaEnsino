using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api1.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Query.TryGetValue(Configurations.ApiKeyName, out var extratedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada."
                };
                return;
            }

            if (!Configurations.ApiKey.Equals(extratedApiKey)) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "ApiKey não autorizada."
                };
                return;
            }

            await next();
        }

       
    }
}
