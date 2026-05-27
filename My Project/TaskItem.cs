using System;
using System.Collections.Generic;

namespace ProjectForSecondSemester {
  // Класс, представляющий задачу
  public class TaskItem {
    private readonly List<ITaskObserver> observers;  // Список подписчиков

    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime Deadline { get; private set; }
    public string Status { get; private set; }

    public TaskItem(string title, string description, DateTime deadline) {
      Title = title;
      Description = description;
      Deadline = deadline;
      Status = "Новая";
      observers = new List<ITaskObserver>();  // Инициализируем пустой список
    }

    // Подписать наблюдателя
    public void Attach(ITaskObserver observer) {
      observers.Add(observer);
      Console.WriteLine($"Наблюдатель подписан на задачу '{Title}'");
    }

    // Отписать наблюдателя
    public void Detach(ITaskObserver observer) {
      observers.Remove(observer);
      Console.WriteLine($"Наблюдатель отписан от задачи '{Title}'");
    }

    // Уведомить всех подписчиков о событии
    private void NotifyObservers(string eventDescription) {
      // Проходим по всем подписчикам через foreach (без лямбд!)
      foreach (ITaskObserver observer in observers) {
        observer.Update(this, eventDescription);
      }
    }

    public void ChangeStatus(string newStatus) {
      if (Status != newStatus) {
        string oldStatus = Status;
        Status = newStatus;

        // Уведомляем всех подписчиков об изменении
        NotifyObservers($"Статус изменен с '{oldStatus}' на '{newStatus}'");
      }
    }

    public void DisplayInfo() {
      Console.WriteLine($"Задача: {Title}");
      Console.WriteLine($"Описание: {Description}");
      Console.WriteLine($"Дедлайн: {Deadline:dd.MM.yyyy HH:mm}");
      Console.WriteLine($"Статус: {Status}");
      Console.WriteLine($"Подписчиков: {observers.Count}");
      Console.WriteLine(new string('-', 30));
    }
  }
}
