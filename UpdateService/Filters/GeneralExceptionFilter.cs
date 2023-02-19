using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UpdateService.Filters
{
    public class GeneralExceptionFilerAttribute : Attribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            using (StreamWriter writer = new StreamWriter("exceptions_log.log", true))
            {
               await writer.WriteLineAsync($"{DateTime.UtcNow} {context.Exception.Message}");
               await writer.WriteLineAsync($"{context.Exception.StackTrace}");
            }
            //context.Result = new BadRequestResult();
        }
    }
}
