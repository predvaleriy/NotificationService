namespace NotificationService.Models;

// record — это лаконичный класс для хранения данных. 
// Компилятор сам создаст конструктор и свойства только для чтения.
public record NotificationLog(
    string Channel,      // Канал отправки (Email или SMS)
    string Recipient,    // Получатель
    string Message,      // Текст сообщения
    bool IsSuccess,      // Успешно ли отправлено (true / false)
    DateTime SentAt      // Дата и время отправки
);