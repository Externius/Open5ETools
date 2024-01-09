using System.Resources;

namespace Open5ETools.Core.Common.Exceptions;
public static class ServiceExceptionExtensions
{
    public static string? LocalizedMessage(this ServiceException serviceException, ResourceManager resourceManager)
    {
        if (string.IsNullOrEmpty(serviceException?.Message))
            return null;

        if (resourceManager is null)
            return serviceException.Message;
        try
        {
            return serviceException.Args != null ? string.Format(resourceManager.GetString(serviceException.Message) ?? string.Empty,
                serviceException.Args) : resourceManager.GetString(serviceException.Message);
        }
        catch (Exception)
        {
            // ignored
        }

        return serviceException.Message;
    }

    public static string? LocalizedMessage(this Exception exception)
    {
        var message = "Internal Generic Error";

        switch (exception)
        {
            case ServiceAggregateException aex:
                message = string.Join(" ", aex.GetInnerExceptions().Select(serviceException => serviceException.LocalizedMessage(Resources.Error.ResourceManager)));
                break;
            case ServiceException ex:
                message = ex.LocalizedMessage(Resources.Error.ResourceManager);
                break;
        }

        return message;
    }
}