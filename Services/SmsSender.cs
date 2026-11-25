// Подключаем пространство имён Models для доступа к Notification и SmsNotification
using NotificationService.Models;

// Указываем пространство имён папки Services
namespace NotificationService.Services;

// Объявляем класс SmsSender, реализующий интерфейс INotificationSender
public class SmsSender : INotificationSender
// Открываем тело класса SmsSender
{
    // Реализуем метод Send для отправки SMS
    public bool Send(Notification notification)
    // Открываем тело метода Send
    {
        // Проверяем: если переданное уведомление НЕ является (is not) типом SmsNotification
        if (notification is not SmsNotification sms)
        // Открываем блок if
        {
            // Сообщаем в консоль, что SMS-отправщик не может отправить письмо или другой тип уведомления
            Console.WriteLine("[Ошибка SmsSender] Неверный тип уведомления. Ожидалось SMS.");
            // Возвращаем false (отправка провалена)
            return false;
        // Закрываем блок if
        }

        // Имитируем отправку SMS через сотового оператора
        Console.WriteLine($"[SMS отправлено] На номер: {sms.Recipient} | Текст: {sms.Message}");
        // Возвращаем true (успешная отправка)
        return true;
    // Закрываем тело метода Send
    }
// Закрываем тело класса SmsSender
}