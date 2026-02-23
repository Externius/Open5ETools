namespace Open5ETools.Web.Models.Home;

public class ErrorViewModel
{
    public required string RequestId { get; init; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}