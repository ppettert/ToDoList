using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Data;
using static System.Console;

namespace ToDoList
{
    // Acts as controller of the model which is
    // the contained TaskList and it's TodoTask items.
    public class TodoListControl : IListControl 
    {
        public List<TodoTask> TaskList { get; set; } = [];

        public void Add( string description, string dueDate, string project )
        {
            TaskList.Add( new TodoTask( description, dueDate, project, TaskStatus.Pending, false ) );
        }

        public void Edit( int index, string propertyString, string propertyName )
        {
            TaskList.ElementAt( index ).EditProperty( propertyString, propertyName );
        }

        public void Delete( int index )
        {
            TaskList.RemoveAt(index);
        }

        public bool SetStatus( int index )
        {
            var task = TaskList.ElementAt( index );
            
            if( !task.Cancelled )
            {
                task.ChangeStatus();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Cancel( int index )
        {
           var task = TaskList.ElementAt( index );
           task.ToggleCancel();
           return task.Cancelled;
        }

        public int Count()
        {
            return TaskList.Count;
        }

        public int IndexOf( TodoTask task )
        {
            return TaskList.IndexOf( task );
        }

        // public void PrintList()
        // {
        //     PrintList(TaskList);
        // }        

        public string ToYamlString()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            return serializer.Serialize(this);
        }

        public static TodoListControl FromYamlString(string yaml)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<TodoListControl>(yaml);
        }


        public List<TodoTask> ToSortedList()
        {
            try 
            {
                return TaskList.OrderBy( x => x.Cancelled )
                               .ThenBy( x => x.Status )
                               .ThenBy( x => DateOnly.Parse( x.DueDate ) )
                               .ToList();
            }
            catch( System.FormatException e )
            {
                WriteLine( "Invalid date field: " + e.Message );
                return [];
            }

        }

        public TodoListControl()
        {    
        }

        public TodoListControl( string yaml )
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            TaskList = deserializer.Deserialize<TodoListControl>(yaml).TaskList;
        }

    }
}