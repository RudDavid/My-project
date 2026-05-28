using System;

namespace My_Project {
  public class ActivityLogger : ITaskObserver {
    // Реализуем метод интерфейса ITaskObserver
    public void Update(TaskItem task, string eventDescription) {
      // Выводим сообщение в консоль с временной меткой
      Console.WriteLine($"[ЛОГ] [{DateTime.Now:HH:mm:ss}] Задача '{task.Title}': {eventDescription}");
    }
  }
}