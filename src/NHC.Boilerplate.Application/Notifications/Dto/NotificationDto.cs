using Abp.Application.Services.Dto;
using System;

namespace NHC.Boilerplate.Notifications.Dto;
public class NotificationRequestDto : PagedResultRequestDto
{
}

public class NotificationDto
{
    public long NotificationId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTimeOffset NotificationDateTime { get; set; }
}
