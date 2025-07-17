using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NHC.Boilerplate.Notifications.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Notifications;
public class NotificationManager : DomainService, ITransientDependency
{
    private readonly IRepository<Notification, long> _notificationRepository;

    public NotificationManager(IRepository<Notification, long> notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }


    public async Task<Notification> CreateNotificationAsync(long userId, string titleAr, string titleEn, string messageAr, string messageEn, NotificationType notificationType)
    {
        var notification = Notification.Create(userId, titleAr, titleEn, messageAr, messageEn, notificationType, DateTime.Now);
        await _notificationRepository.InsertAsync(notification);

        return notification;
    }

    public async Task<int> GetUnreadCountAsync(long userId)
    {
        return await _notificationRepository.CountAsync(x =>
            x.UserId == userId && !x.IsRead && !x.IsDeleted
        );
    }

    public async Task<bool> MarkAsReadAsync(long notificationId, long userId)
    {
        var notification = await _notificationRepository.FirstOrDefaultAsync(x => x.Id == notificationId && x.UserId == userId);

        if (notification == null)
            throw new UserFriendlyException(L("InvalidNotificationNumber"));

        notification.MarkAsRead();
        return true;
    }

    public async Task<bool> DeleteAsync(long notificationId, long userId)
    {
        var notification = await _notificationRepository.FirstOrDefaultAsync(x => x.Id == notificationId && x.UserId == userId);

        if (notification == null)
            throw new UserFriendlyException(L("InvalidNotificationNumber"));

        await _notificationRepository.DeleteAsync(notification);
        return true;
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(long userId)
    {
        return await _notificationRepository.GetAll()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }

}
