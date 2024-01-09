
namespace Open5ETools.Web.Models.Home;

public class ErrorViewModel
{
    public string RequestId { get; set; } = string.Empty;

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}