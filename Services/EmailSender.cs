
// Подключаем пространство имён Models, чтобы использовать классы Notification и EmailNotification
using NotificationService.Models;

// Указываем пространство имён текущей папки Services
namespace NotificationService.Services;

// Объявляем класс EmailSender и указываем (: INotificationSender), что он обязуется выполнить контракт интерфейса
public class EmailSender : INotificationSender
// Открываем тело класса EmailSender
{
    // Реализуем обязательный метод Send из интерфейса INotificationSender
    public bool Send(Notification notification)
    // Открываем тело метода Send
    {
        // Проверяем: если переданное уведомление НЕ является (is not) письмом EmailNotification
        if (notification is not EmailNotification email)
        // Открываем блок if
        {
            // Выводим в консоль сообщение об ошибке, что этот отправщик умеет работать только с Email
            Console.WriteLine("[Ошибка EmailSender] Неверный тип уведомления. Ожидался Email.");
            // Возвращаем false (сигнал о том, что отправка не удалась)
            return false;
        // Закрываем блок if
        }

        // Имитируем реальную отправку письма через почтовый сервер, выводя детали в консоль
        Console.WriteLine($"[Email отправлен] Кому: {email.Recipient} | Тема: {email.Subject} | Текст: {email.Message}");
        // Возвращаем true (сигнал о том, что письмо успешно отправлено)
        return true;
    // Закрываем тело метода Send
    }
// Закрываем тело класса EmailSender
}