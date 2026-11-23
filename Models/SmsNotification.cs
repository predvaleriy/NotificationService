namespace NotificationService.Models;

// Наследуемся от базового класса Notification
public class SmsNotification : Notification
{
    // Конструктор принимает только получателя и текст, так как темы у SMS нет
    public SmsNotification(string recipient, string message)
        : base(recipient, message)
    {
        // Бизнес-правило конкретно для SMS-канала
        if (message.Length > 160)
            throw new ArgumentException("SMS не может быть длиннее 160 символов.", nameof(message));
    }

    // Своя реализация вывода информации для SMS
    public override string GetSummary()
    {
        return $"[SMS] Кому: {Recipient} | Текст: {Message}";
    }
}