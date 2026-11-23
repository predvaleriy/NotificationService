using NotificationService.Models;

// Создаём тестовые уведомления
var email = new EmailNotification("ivan@mail.ru", "Добро пожаловать", "Спасибо за регистрацию!");
var sms = new SmsNotification("+79991234567", "Ваш код подтверждения: 4815");

Console.WriteLine(email.GetSummary());
Console.WriteLine(sms.GetSummary());