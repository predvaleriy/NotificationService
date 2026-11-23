namespace NotificationService.Models;

// abstract — запрещает создавать объект new Notification() напрямую.
// От этого класса можно только наследоваться.
public abstract class Notification
{
    // Guid — уникальный идентификатор (например, "550e8400-e29b-41d4-a716-446655440000").
    // get; — читать можно всем; private set; — менять можно только внутри этого класса (инкапсуляция).
    public Guid Id { get; private set; }
    public string Recipient { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Конструктор с валидацией (защитой от "мусорных" данных)
    protected Notification(string recipient, string message)
    {
        if (string.IsNullOrWhiteSpace(recipient))
            throw new ArgumentException("Получатель не может быть пустым.", nameof(recipient));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Текст сообщения не может быть пустым.", nameof(message));

        Id = Guid.NewGuid();
        Recipient = recipient;
        Message = message;
        CreatedAt = DateTime.UtcNow; // В бэкенде время всегда хранят в UTC (по Гринвичу)
    }

    // virtual — разрешаем наследникам переопределить этот метод под себя (полиморфизм)
    public virtual string GetSummary()
    {
        return $"[{CreatedAt:HH:mm:ss}] Кому: {Recipient} | Текст: {Message}";
    }
}