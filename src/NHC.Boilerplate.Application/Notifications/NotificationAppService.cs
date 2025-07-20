﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Notifications.Dto;

[AbpAuthorize]
[Route("api/notifications")]
public class NotificationAppService(NotificationManager _notificationManager) : BoilerplateAppServiceBase
{
    [HttpGet("all")]
    public async Task<PagedResultDto<NotificationDto>> GetAllAsync(NotificationRequestDto request)
    {
        var currentUser = await GetCurrentUserAsync();
        var allNotifications = await _notificationManager.GetUserNotificationsAsync(currentUser.Id);

        var pagedNotifications = allNotifications
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount)
            .ToList();

        var notificationDtos = pagedNotifications
            .Select(notification => new NotificationDto
            {
                NotificationId = notification.Id,
                Title = notification.TitleAr,
                Message = notification.MessageAr,
                IsRead = notification.IsRead,
                NotificationDateTime = notification.NotificationDateTime
            })
            .ToList();

        return new PagedResultDto<NotificationDto>
        {
            Items = notificationDtos,
            TotalCount = allNotifications.Count
        };
    }

    [HttpGet("count")]
    public async Task<int> GetCountAsync()
    {
        var currentUser = (await GetCurrentUserAsync()).Id;
        return await _notificationManager.GetUnreadCountAsync(currentUser);
    }

    [HttpPost("mark-read/{notificationId}")]
    public async Task<bool> MarkAsReadAsync(long notificationId)
    {
        var currentUser = (await GetCurrentUserAsync()).Id;
        return await _notificationManager.MarkAsReadAsync(notificationId, currentUser);
    }

    [HttpPost("{notificationId}")]
    public async Task<bool> DeleteAsync(long notificationId)
    {
        var currentUser = (await GetCurrentUserAsync()).Id;
        return await _notificationManager.DeleteAsync(notificationId, currentUser);
    }
}
