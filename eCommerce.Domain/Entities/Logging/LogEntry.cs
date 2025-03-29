using Domain.Entities.Common;

namespace Domain.Entities.Logging;

public class LogEntry : IBaseEntity
{
    public int Id { get; set; }
    public string RequestId { get; set; }
    public string RequestName { get; set; }
    public DateTime Timestamp { get; set; }
    public long ElapsedTime { get; set; }
    public string Status { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public string RequestData { get; set; }
    public string ResponseData { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string IpAddress { get; set; }
}