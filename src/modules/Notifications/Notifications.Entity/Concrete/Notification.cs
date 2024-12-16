using Core.Entities;

namespace Notifications.Entity.Concrete
{
    public class Notification : IEntity
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }  
        public string Title { get; set; }
        public string Message { get; set; } 

        public NotificationType Type { get; set; }  
        public bool IsRead { get; set; }  

        public DateTime CreatedAt { get; set; }  
        public DateTime? ReadAt { get; set; }  
    }

    public enum NotificationType
    {
        System = 1,     // Sistem bildirimleri
        Message = 2,    // Mesaj bildirimi
        Reminder = 3    // Hatırlatma bildirimi
    }
}
