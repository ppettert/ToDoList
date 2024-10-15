using Microsoft.VisualBasic;

namespace ToDoList
{
    // Acts as controller of the model which is
    // the contained TaskList and it's TodoTask items.
    public class TodoListControl : IListControl 
    {
        public List<TodoTask>? TaskList { get; set; }


        public void Add( string description, string dueDate, string project )
        {
            TaskList?.Add( new TodoTask( description, dueDate, project, TaskStatus.Pending, false ) );
        }

        public void Edit( int index, string propertyString, string propertyName )
        {
            TaskList?.ElementAt( index ).EditProperty( propertyString, propertyName );
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