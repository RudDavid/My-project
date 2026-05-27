namespace My_Project {
  public interface ITaskObserver {
    // Метод, который будет вызываться при изменении задачи
    // task - сама задача, которая изменилась
    // eventDescription - описание того, что произошло
    void Update(TaskItem task, string eventDescription);
  }
}
