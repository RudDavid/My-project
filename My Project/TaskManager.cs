using System;
using System.Collections.Generic;

namespace My_Project {
  // Класс для управления списком задач
  public class TaskManager {
    private List<TaskItem> tasks;

    public TaskManager() {
      tasks = new List<TaskItem>();
    }

    // Добавить задачу в список
    public void AddTask(TaskItem task) {
      tasks.Add(task);
    }

    // Получить все задачи
    public List<TaskItem> GetAllTasks() {
      return tasks;
    }

    // Найти задачу по номеру в списке
    public TaskItem GetTaskByIndex(int index) {
      if (index >= 0 && index < tasks.Count) {
        return tasks[index];
      }
      return null;
    }

    // Количество задач
    public int Count {
      get { return tasks.Count; }
    }

    // Вывести все задачи с номерами
    public void DisplayAllTasks() {
      if (tasks.Count == 0) {
        Console.WriteLine("Список задач пуст.");
        return;
      }

      for (int i = 0; i < tasks.Count; i++) {
        Console.WriteLine($"[{i}] {tasks[i].Title} | Статус: {tasks[i].Status} | Дедлайн: {tasks[i].Deadline:dd.MM.yyyy HH:mm}");
      }
    }
  }
}