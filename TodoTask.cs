

namespace ToDoList
{

    /*
     *  Enum for TodoTask state
     */
    public enum TaskStatus
    {
        Pending,
        Completed
    }

    /*
     *  
     */
    public class TodoTask
    {
        public string? Description { get; set; } 
        public string DueDate { get; set; } = "";
        public TaskStatus Status { get; set; } 
        public string? Project { get; set; } 
        public bool Cancelled { get; set; }

        /*
         * TodoTask Class constructor, creates a new TodoTask instance
         */
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

        /* 
         *  EditProperty method, to edit the properties of a TodoTask
         *
         *  Parameters:
         *
         *  propertyString  string  The property value
         *  propertyName    string  The name of the property to change
         *    
         *  returns:    false if propertyName does not match any TodoTask property
         *              true if the property was changed successfully
         */
        public bool EditProperty( string propertyString, string propertyName )
        {   
            propertyName = propertyName.ToLower();

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