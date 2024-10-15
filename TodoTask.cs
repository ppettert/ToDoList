

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
        public string DueDate { get; set; } = "";
        public TaskStatus Status { get; set; } 
        public string? Project { get; set; } 
        public bool Cancelled { get; set; }

        public TodoTask()
        {
           // Required by yaml deserialize!
        }
        public TodoTask( string description, string dueDate, string project, TaskStatus status=TaskStatus.Pending, bool cancelled = false ) 
        {
            Description = description;
            DueDate = dueDate;
            Status = status;
            Project = project;
            Cancelled = cancelled;
        }

        /* returns false if propertyName does not match any TodoTask property */
        public bool EditProperty( string propertyString, string propertyName )
        {
            switch( propertyName )
            {
                case "description":
                    Description = propertyString;
                    break;
                case "duedate":
                    DueDate = propertyString;
                    break;
                case "project":
                    Project = propertyString;
                    break;
                default:
                    return false; 
            }
            return true;

        }

        /* Toggles between Pending and Completed */
        public void ChangeStatus()
        {
            if( Status == TaskStatus.Pending )
                Status = TaskStatus.Completed;
            else
                Status = TaskStatus.Pending;

        }

        public void ToggleCancel()
        {
            Cancelled = !Cancelled;
        }
    }
}