using Core.Enums;

namespace Core.Models;

public class Error
{
    public ErrorType ErrorCode { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}