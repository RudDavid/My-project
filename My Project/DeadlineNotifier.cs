using System;

namespace My_Project {
  public class DeadlineNotifier : ITaskObserver {
    // Порог предупреждения - за сколько часов до дедлайна начинать беспокоиться
    private readonly int warningHours;

    public DeadlineNotifier(int warningHours) {
      this.warningHours = warningHours;
    }

    // Реализуем метод интерфейса ITaskObserver
    public void Update(TaskItem task, string eventDescription) {
      // Проверяем только если задача ещё не выполнена
      if (task.Status != "Выполнена") {
        CheckDeadline(task);
      }
    }

    private void CheckDeadline(TaskItem task) {
      DateTime now = DateTime.Now;
      TimeSpan timeLeft = task.Deadline - now;

      // Дедлайн уже прошел
      if (timeLeft.TotalHours < 0) {
        double hoursOverdue = Math.Abs(timeLeft.TotalHours);
        Console.WriteLine($"[УВЕДОМЛЕНИЕ] ПРОСРОЧЕНО! Задача '{task.Title}' просрочена на {hoursOverdue:F1} ч.");
      }
      // Дедлайн в пределах порога предупреждения
      else if (timeLeft.TotalHours <= warningHours) {
        Console.WriteLine($"[УВЕДОМЛЕНИЕ] ВНИМАНИЕ! Задача '{task.Title}': осталось {timeLeft.TotalHours:F1} ч. до дедлайна!");
      }
    }
  }
}
