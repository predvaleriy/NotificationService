// Подключаем пространство имён Models, чтобы этот файл "видел" наш класс Notification
using NotificationService.Models;

// Указываем, что этот файл относится к папке (пространству имён) Services
namespace NotificationService.Services;

// Объявляем публичный интерфейс INotificationSender (контракт для всех отправщиков)
public interface INotificationSender
// Открываем тело интерфейса
{
    // Описываем правило: любой класс, реализующий этот интерфейс, обязан иметь метод Send,
    // который принимает любое уведомление (Notification) и возвращает результат (true — отправлено, false — ошибка)
    bool Send(Notification notification);
// Закрываем тело интерфейса
}