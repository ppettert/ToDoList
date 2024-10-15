namespace ToDoList
{

    public enum TaskStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class TodoTask
    {
      //   private DateOnly _dueDate = dueDate;

        public string? TaskTitle { get; set; } 
        public string? DueDate { get; set; } 
        public TaskStatus Status { get; set; } 
        public string? Project { get; set; } 

        public TodoTask()
        {
           // Needed by yaml deserialize 
        }
        public TodoTask( string taskTitle, string dueDate, TaskStatus status, string project ) 
        {
            TaskTitle = taskTitle;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }
    }
}