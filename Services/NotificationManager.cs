// Подключаем пространство имён для работы с JSON
using System.Text.Json;
// Подключаем наши модели
using NotificationService.Models;

namespace NotificationService.Services;

public class NotificationManager
{
    // 1. СЛОВАРЬ (Dictionary): связывает Тип уведомления (Ключ) с нужным Отправщиком (Значение)
    private readonly Dictionary<Type, INotificationSender> _senders = new();

    // 2. МНОЖЕСТВО (HashSet): хранит уникальные отпечатки отправленных сообщений для защиты от дублей
    private readonly HashSet<string> _sentHashes = new();

    // 3. СПИСОК (List): хранит полную историю всех попыток отправки
    private readonly List<NotificationLog> _history = new();

    // Метод регистрации нового канала отправки в нашем менеджере
    public void RegisterSender<TNotification>(INotificationSender sender) 
        where TNotification : Notification
    {
        // typeof(TNotification) получает тип класса (например, EmailNotification)
        _senders[typeof(TNotification)] = sender;
    }

    // Главный метод: принять любое уведомление, проверить и отправить
    public void Process(Notification notification)
    {
        // Формируем уникальный "отпечаток" сообщения: Тип + Получатель + Текст
        string uniqueKey = $"{notification.GetType().Name}:{notification.Recipient}:{notification.Message}";

        // Проверяем в HashSet: метод Add вернёт false, если такой ключ там УЖЕ есть
        if (!_sentHashes.Add(uniqueKey))
        {
            Console.WriteLine($"[Дубль заблокирован] Сообщение для {notification.Recipient} уже было отправлено ранее.");
            return; // Прерываем работу метода, не отправляем повторно
        }

        // Получаем реальный тип пришедшего объекта (EmailNotification или SmsNotification)
        Type notificationType = notification.GetType();

        // Ищем в словаре подходящий отправщик по типу уведомления
        if (_senders.TryGetValue(notificationType, out INotificationSender? sender))
        {
            // Отправляем через найденный сервис (Полиморфизм!)
            bool isSuccess = sender.Send(notification);

            // Записываем результат в историю (List)
            var logEntry = new NotificationLog(
                Channel: notificationType.Name,
                Recipient: notification.Recipient,
                Message: notification.Message,
                IsSuccess: isSuccess,
                SentAt: DateTime.Now
            );
            _history.Add(logEntry);
        }
        else
        {
            Console.WriteLine($"[Ошибка маршрутизации] Не найден отправщик для типа {notificationType.Name}");
        }
    }

    // Метод сохранения всей истории рассылок в JSON-файл
    public void SaveHistoryToJson(string filePath)
    {
        // Настраиваем красивое форматирование JSON (с отступами и поддержкой кириллицы)
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // Превращаем List<NotificationLog> в JSON-строку
        string jsonString = JsonSerializer.Serialize(_history, options);

        // Записываем строку в файл на диск
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine($"\n[Система] История ({_history.Count} шт.) сохранена в файл: {filePath}");
    }

    // Метод для получения копии истории (пригодится нам в Уроке 4 для LINQ!)
    public List<NotificationLog> GetHistory() => _history;
}