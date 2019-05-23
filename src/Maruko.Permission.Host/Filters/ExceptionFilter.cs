using Maruko.Application;
using Maruko.Permission.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maruko.Permission.Host.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = (context.Exception as MarukoException) ?? new MarukoException(context.Exception.Message, ServiceEnum.Failure);

            var response = new ApiReponse<object>
            {
                Msg = exception.Msg,
                Status = exception.Status
            };
            context.Result = new JsonResult(response);
        }
    }
}
