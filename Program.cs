using NotificationService.Models;
using NotificationService.Services;

// 1. Создаём наш менеджер рассылок
var manager = new NotificationManager();

// 2. Регистрируем в нём доступные каналы (настраиваем Словарь)
manager.RegisterSender<EmailNotification>(new EmailSender());
manager.RegisterSender<SmsNotification>(new SmsSender());

// 3. Создаём пачку разных уведомлений, включая один случайный дубль
var queue = new List<Notification>
{
    new EmailNotification("ivan@mail.ru", "Счёт №105", "Ваш счёт сформирован."),
    new SmsNotification("+79991234567", "Код подтверждения: 9912"),
    new EmailNotification("ivan@mail.ru", "Счёт №105", "Ваш счёт сформирован."), // Дубль!
    new EmailNotification("anna@yandex.ru", "Добро пожаловать", "Спасибо за регистрацию!"),
    new SmsNotification("+79005553322", "Ваш заказ передан курьеру.")
};

Console.WriteLine("=== Запуск пакетной рассылки ===\n");

// 4. Прогоняем всю очередь через менеджер
foreach (var item in queue)
{
    manager.Process(item);
}

// 5. Сохраняем отчёт о работе в JSON-файл в папке проекта
manager.SaveHistoryToJson("history.json");