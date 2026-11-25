// Подключаем наши модели (EmailNotification, SmsNotification)
using NotificationService.Models;
// Подключаем наши сервисы отправки (INotificationSender, EmailSender, SmsSender)
using NotificationService.Services;

// Создаём список уведомлений (здесь пока просто создаём два разных объекта)
var email = new EmailNotification("ivan@mail.ru", "Счёт на оплату", "Ваш счёт №105 сформирован.");
var sms = new SmsNotification("+79991234567", "Код входа: 9912");

// Создаём конкретные отправщики, но записываем их в переменные типа интерфейса INotificationSender (Абстракция!)
INotificationSender emailSender = new EmailSender();
INotificationSender smsSender = new SmsSender();

// Вызываем один и тот же метод Send() у обоих отправщиков — каждый сработает по-своему (Полиморфизм!)
emailSender.Send(email);
smsSender.Send(sms);

// Проверим защиту: попробуем отправить SMS через EmailSender (должно вывести ошибку и не упасть)
emailSender.Send(sms);