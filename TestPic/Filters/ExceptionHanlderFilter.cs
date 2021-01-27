using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TestPic.Filters
{
    public class ExceptionHanlderFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var Test = context.Exception.InnerException;

            var RealException = context.Exception;
            var TempAddress = context.ActionDescriptor.DisplayName.Split('.').ToList();
            var Address = TempAddress[TempAddress.Count - 2] + "." + TempAddress[TempAddress.Count - 1].Split(' ')[0];
            while (RealException.InnerException != null)
                RealException = RealException.InnerException;
            var State = new StackTrace(RealException, true);
            var line = State.GetFrame(0).GetFileLineNumber();

            context.Result = new JsonResult(new
            {
                Message = RealException.Message,
                Source = $"in {Address}, Line: {line}"
            });
            // Get the line number from the stack frame
            context.HttpContext.Response.StatusCode = 500;
            return base.OnExceptionAsync(context);
        }
    }
}
