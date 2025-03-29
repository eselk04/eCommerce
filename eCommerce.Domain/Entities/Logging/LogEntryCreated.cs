using MediatR;

namespace Domain.Entities.Logging;

public class LogEntryCreated : INotification
{
    public string RequestId { get; }
    public string RequestName { get; }
    public DateTime Timestamp { get; }
    public long ElapsedTime { get; }
    public string Status { get; }
    public string ErrorMessage { get; }
    public string StackTrace { get; }
    public string RequestData { get; }
    public string ResponseData { get; }
    public string UserId { get; }
    public string UserName { get; }
    public string IpAddress { get; }

    public LogEntryCreated(
        string requestId, 
        string requestName, 
        DateTime timestamp, 
        long elapsedTime,
        string status, 
        string errorMessage = null, 
        string stackTrace = null, 
        string requestData = null, 
        string responseData = null,
        string userId = null,
        string userName = null,
        string ipAddress = null)
    {
        RequestId = requestId;
        RequestName = requestName;
        Timestamp = timestamp;
        ElapsedTime = elapsedTime;
        Status = status;
        ErrorMessage = errorMessage;
        StackTrace = stackTrace;
        RequestData = requestData;
        ResponseData = responseData;
        UserId = userId;
        UserName = userName;
        IpAddress = ipAddress;
    }
}