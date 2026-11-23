namespace NotificationService.Models;

// Двоеточие (: Notification) означает наследование от базового класса Notification.
// EmailNotification автоматически получает Id, Recipient, Message и CreatedAt.
public class EmailNotification : Notification
{
    // Уникальное свойство, которое есть только у письма, но нет у SMS
    public string Subject { get; private set; }

    // : base(recipient, message) — сначала передаём общие параметры в конструктор родителя.
    // Родитель сам проверит, что recipient и message не пустые, и сгенерирует Id.
    public EmailNotification(string recipient, string subject, string message)
        : base(recipient, message)
    {
        // Инкапсуляция: защищаем специфичное поле Email от некорректных данных
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Тема письма не может быть пустой.", nameof(subject));

        Subject = subject;
    }

    // override — переопределяем виртуальный (virtual) метод родителя.
    // Это проявление полиморфизма: у каждого типа уведомления свой формат вывода.
    public override string GetSummary()
    {
        return $"[Email] Кому: {Recipient} | Тема: {Subject} | Текст: {Message}";
    }
}