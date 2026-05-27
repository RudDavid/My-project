using System;

namespace ProjectForSecondSemester {
  internal class Program {
    public static void Main() {
      Console.WriteLine("=== Тестирование DeadlineNotifier ===\n");

      // Создаем логгер (будет общим для всех задач)
      ActivityLogger logger = new ActivityLogger();

      // Создаем уведомитель (предупреждать за 24 часа до дедлайна)
      DeadlineNotifier notifier = new DeadlineNotifier(24);

      Console.WriteLine("=== Создаем задачи ===\n");

      // Задача 1: дедлайн уже прошел (вчера)
      TaskItem task1 = new TaskItem(
          "Сдать старую лабораторную",
          "Эту работу нужно было сдать ещё вчера",
          DateTime.Now.AddDays(-1)
      );
      task1.Attach(logger);
      task1.Attach(notifier);

      // Задача 2: дедлайн скоро (через 1 минуту для теста)
      TaskItem task2 = new TaskItem(
          "Срочная задача",
          "Дедлайн почти наступил",
          DateTime.Now.AddMinutes(1)
      );
      task2.Attach(logger);
      task2.Attach(notifier);

      // Задача 3: дедлайн не скоро (через 7 дней)
      TaskItem task3 = new TaskItem(
          "Курсовой проект",
          "Времени ещё много",
          DateTime.Now.AddDays(7)
      );
      task3.Attach(logger);
      task3.Attach(notifier);

      // Выводим информацию о задачах
      Console.WriteLine("Текущее состояние задач:");
      task1.DisplayInfo();
      task2.DisplayInfo();
      task3.DisplayInfo();

      // Теперь меняем статусы и смотрим на реакцию
      Console.WriteLine("=== Изменяем статус просроченной задачи ===\n");
      task1.ChangeStatus("В работе");

      Console.WriteLine("\n=== Изменяем статус срочной задачи ===\n");
      task2.ChangeStatus("В работе");

      Console.WriteLine("\n=== Изменяем статус обычной задачи ===\n");
      task3.ChangeStatus("В работе");

      // Выполняем срочную задачу и проверяем, что уведомления прекратились
      Console.WriteLine("\n=== Выполняем срочную задачу ===\n");
      task2.ChangeStatus("Выполнена");

      Console.WriteLine("Нажми любую клавишу для выхода...");
      _ = Console.ReadKey();
    }
  }
}
