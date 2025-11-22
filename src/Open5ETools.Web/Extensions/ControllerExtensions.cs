using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Exceptions;

namespace Open5ETools.Web.Extensions;

public static class ControllerExtensions
{
    public static void HandleException(this Controller controller, Exception ex, ILogger logger,
        string? defaultError = null)
    {
        switch (ex)
        {
            case ServiceAggregateException ae:
            {
                foreach (var exception in ae.GetInnerExceptions())
                {
                    controller.ModelState.AddModelError(exception.Field, exception.Message);
                }

                break;
            }
            case ServiceException e:
                controller.ModelState.AddModelError(e.Field, e.Message);
                break;
            default:
                logger.LogError(ex, "{GetDisplayUrl}: {DefaultError}", controller.Request.GetDisplayUrl(),
                    defaultError ?? ServiceException.GeneralError);
                controller.ModelState.AddModelError(string.Empty, defaultError ?? ServiceException.GeneralError);
                break;
        }
    }
}