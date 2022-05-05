using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using UserSwipeAssignment.Utilities;

namespace UserSwipeAssignment.Filters
{
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);

            var actionType = actionExecutedContext.Exception.GetType();

            HttpResponseMessage response;
            
            if (actionType == typeof(UnauthorizedAccessException))
            {
                response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("User not authenticated!!!")
                };
            }
            else if (actionType == typeof(NullReferenceException))
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("null refernece exception found!!!")
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An unhandled exception was thrown by controller: " +
                    actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName
                    + " and In action: " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName),
                    ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
                };
            }

            actionExecutedContext.Response = response;

            base.OnException(actionExecutedContext);
        }
    }
}