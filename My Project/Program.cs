using My_Project;
using System;

namespace My_Project {
  class Program {
    static TaskManager taskManager;
    static ActivityLogger logger;
    static DeadlineNotifier notifier;

    static void Main(string[] args) {
      // Инициализируем менеджер и подписчиков
      taskManager = new TaskManager();
      logger = new ActivityLogger();
      notifier = new DeadlineNotifier(24);  // Предупреждать за 24 часа

      Console.WriteLine("=== TaskFlow - Менеджер задач ===\n");

      bool running = true;

      while (running) {
        // Главное меню
        Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
        Console.WriteLine("1. Создать задачу");
        Console.WriteLine("2. Показать все задачи");
        Console.WriteLine("3. Показать детали задачи");
        Console.WriteLine("4. Изменить статус задачи");
        Console.WriteLine("5. Выйти");
        Console.Write("Выберите действие: ");

        string choice = Console.ReadLine();

        switch (choice) {
          case "1":
            CreateTask();
            break;
          case "2":
            ShowAllTasks();
            break;
          case "3":
            ShowTaskDetails();
            break;
          case "4":
            ChangeTaskStatus();
            break;
          case "5":
            running = false;
            Console.WriteLine("До свидания!");
            break;
          default:
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            break;
        }
      }
    }

    // Создание новой задачи
    static void CreateTask() {
      Console.WriteLine("\n=== СОЗДАНИЕ ЗАДАЧИ ===");

      Console.Write("Название: ");
      string title = Console.ReadLine();

      Console.Write("Описание: ");
      string description = Console.ReadLine();

      Console.Write("Дедлайн (в часах от текущего момента): ");
      int hours;
      while (!int.TryParse(Console.ReadLine(), out hours)) {
        Console.Write("Введите число: ");
      }

      DateTime deadline = DateTime.Now.AddHours(hours);

      TaskItem newTask = new TaskItem(title, description, deadline);

      // Подписываем наблюдателей
      newTask.Attach(logger);
      newTask.Attach(notifier);

      // Добавляем в менеджер
      taskManager.AddTask(newTask);

      Console.WriteLine($"\nЗадача '{title}' успешно создана!");
      Console.WriteLine($"Дедлайн установлен на: {deadline:dd.MM.yyyy HH:mm}");
    }

    // Показать список всех задач
    static void ShowAllTasks() {
      Console.WriteLine("\n=== СПИСОК ЗАДАЧ ===");
      taskManager.DisplayAllTasks();
    }

    // Показать детали конкретной задачи
    static void ShowTaskDetails() {
      Console.WriteLine("\n=== ДЕТАЛИ ЗАДАЧИ ===");

      if (taskManager.Count == 0) {
        Console.WriteLine("Нет доступных задач.");
        return;
      }

      taskManager.DisplayAllTasks();

      Console.Write("\nВведите номер задачи: ");
      int index;
      while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= taskManager.Count) {
        Console.Write("Введите корректный номер: ");
      }

      TaskItem task = taskManager.GetTaskByIndex(index);
      Console.WriteLine();
      task.DisplayInfo();
    }

    // Изменить статус задачи
    static void ChangeTaskStatus() {
      Console.WriteLine("\n=== ИЗМЕНЕНИЕ СТАТУСА ===");

      if (taskManager.Count == 0) {
        Console.WriteLine("Нет доступных задач.");
        return;
      }

      taskManager.DisplayAllTasks();

      Console.Write("\nВведите номер задачи: ");
      int index;
      while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= taskManager.Count) {
        Console.Write("Введите корректный номер: ");
      }

      TaskItem task = taskManager.GetTaskByIndex(index);

      Console.WriteLine($"\nТекущий статус: {task.Status}");
      Console.WriteLine("Доступные статусы:");
      Console.WriteLine("1. Новая");
      Console.WriteLine("2. В работе");
      Console.WriteLine("3. На проверке");
      Console.WriteLine("4. Выполнена");
      Console.Write("Выберите новый статус: ");

      string statusChoice = Console.ReadLine();
      string newStatus = "";

      switch (statusChoice) {
        case "1":
          newStatus = "Новая";
          break;
        case "2":
          newStatus = "В работе";
          break;
        case "3":
          newStatus = "На проверке";
          break;
        case "4":
          newStatus = "Выполнена";
          break;
        default:
          Console.WriteLine("Неверный выбор статуса.");
          return;
      }

      task.ChangeStatus(newStatus);
    }
  }
}