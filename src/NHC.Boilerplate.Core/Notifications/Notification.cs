﻿using Abp.Domain.Entities.Auditing;
using NHC.Boilerplate.Notifications.Enum;
using System;

namespace NHC.Boilerplate.Notifications;
public class Notification : FullAuditedEntity<long>
{
    public long UserId { get; private set; }
    public string TitleAr { get; private set; }
    public string TitleEn { get; private set; }
    public string MessageAr { get; private set; }
    public string MessageEn { get; private set; }
    public NotificationType NotificationType { get; private set; }
    public bool IsRead { get; private set; }
    public DateTimeOffset NotificationDateTime { get; private set; }

    private Notification() { }

    private Notification(long userId, string titleAr, string titleEn, string messageAr, string messageEn, NotificationType notificationType, DateTimeOffset notificationDateTime)
    {
        UserId = userId;
        TitleAr = titleAr;
        TitleEn = titleEn;
        MessageAr = messageAr;
        MessageEn = messageEn;
        NotificationType = notificationType;
        IsRead = false;
        NotificationDateTime = notificationDateTime;
    }

    public static Notification Create(
        long userId, string titleAr, string titleEn, string messageAr, string messageEn,
        NotificationType type, DateTimeOffset NotificationDateTime)
    {
        return new Notification(userId, titleAr, titleEn, messageAr, messageEn, type, NotificationDateTime);
    }

    public void MarkAsRead()
    {
        if (!IsRead)
            IsRead = true;
    }
}
