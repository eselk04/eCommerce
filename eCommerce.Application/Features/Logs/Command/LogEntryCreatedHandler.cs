using Domain.Entities.Logging;
using eCommerce.Application.Bases;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Serilog;

namespace eCommerce.Application.Features.Logs.Command;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public class LogEntryCreatedHandler : BaseHandler, INotificationHandler<LogEntryCreated>
{
    public LogEntryCreatedHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
    }

    public async Task Handle(LogEntryCreated notification, CancellationToken cancellationToken)
    {
       LogEntry logEntry = mapper.Map<LogEntry,LogEntryCreated>(notification);
     //   var logEntry = new LogEntry
     //   { UserName = notification.UserName,
     //       UserId = notification.UserId,
     //       RequestId = notification.RequestId,
     //       RequestName = notification.RequestName,
     //       Timestamp = notification.Timestamp,
     //       ElapsedTime = notification.ElapsedTime,
     //       Status = notification.Status,
     //       ErrorMessage = notification.ErrorMessage,
     //       StackTrace = notification.StackTrace,
     //       RequestData = notification.RequestData,
     //       ResponseData = notification.ResponseData,
     //       IpAddress = notification.IpAddress,
     //   };

        await unitOfWork.GetWriteRepository<LogEntry>().AddAsync(logEntry);
        await unitOfWork.SaveAsync();
    }
}