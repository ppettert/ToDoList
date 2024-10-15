namespace ToDoList
{

    public enum TaskStatus
    {
        Pending,
        Completed
    }

    public class TodoTask
    {
        public string? Description { get; set; } 
        public string? DueDate { get; set; } 
        public TaskStatus Status { get; set; } 
        public string? Project { get; set; } 
        public bool Cancelled { get; set; }

        public TodoTask()
        {
           // Needed by yaml deserialize 
        }
        public TodoTask( string description, string dueDate, TaskStatus status, string project, bool cancelled = false ) 
        {
            Description = description;
            DueDate = dueDate;
            Status = status;
            Project = project;
            Cancelled = cancelled;
        }

        public void ChangeStatus()
        {
            Status = (Status == TaskStatus.Pending ? TaskStatus.Completed : TaskStatus.Pending);
        }

        public void ToggleCancel()
        {
            Cancelled = !Cancelled;
        }
    }
}