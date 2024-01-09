namespace Open5ETools.Core.Common.Models.Services;

public class EditModel
{
    public int Id { get; set; }
    public byte[] Timestamp { get; set; } = [];
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public string LastModifiedBy { get; set; } = string.Empty;

    public DateTime LastModified { get; set; }
}