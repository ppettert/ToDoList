namespace ToDoList
{

    // This class is needed by Yaml (de)serializer/parser   
    // And also acts as controller of the model which is
    // the contained TaskList and it's TodoTask items.
    public class TodoListControl : IListControl 
    {
        public List<TodoTask>? TaskList { get; set; }

        // public TodoListControl()
        // {
        // }

        // All List/Task actions such as Add, Edit & Toggle, List, 
        //  will be passing through here

        public void Add( string description, string dueDate, string project )
        {
            TaskList?.Add( new TodoTask( description, dueDate, TaskStatus.Pending, project, false ) );
        }


        public void Edit( int index, string propertyString, string propertyName )
        {
            switch( propertyName )
            {
                case "description":
                    break;
                case "duedate":
                    break;
                case "project":
                    break;
                default:
                    break;

            }
           //  TaskList?.ElementAt( index ).
        }

        public void Delete( int index )
        {
            TaskList?.RemoveAt(index);
        }

        public void SetStatus( int index )
        {
            TaskList?.ElementAt( index ).ChangeStatus();
        }

        public void Cancel( int index )
        {
            TaskList?.ElementAt( index).ToggleCancel();
        }


    }
}