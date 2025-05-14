namespace Open5ETools.Core.Common.Exceptions;

public class ServiceException : Exception
{
    public const string GeneralAggregateError = "GeneralAggregateError";
    public const string EntityNotFoundException = "EntityNotFoundException";
    public const string RequiredValidation = "RequiredValidation";
    public const string EncounterNotPossible = "EncounterNotPossible";

    public object[]? Args { get; }

    public string? Field { get; }

    public ServiceException(string message, params object[] args) : base(message)
    {
        Field = null;
        Args = args;
    }

    public ServiceException(string message, string field, params object[] args) : base(message)
    {
        Field = field;
        Args = args;
    }

    public ServiceException(string message, Exception innerException, params object[] args) : base(message,
        innerException)
    {
        Field = null;
        Args = args;
    }

    public ServiceException(string message, string field, Exception innerException, params object[] args) : base(
        message, innerException)
    {
        Field = field;
        Args = args;
    }
}