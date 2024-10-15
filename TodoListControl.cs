using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Data;

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

        public void SetStatus( int index )
        {
            TaskList.ElementAt( index ).ChangeStatus();
        }

        public void Cancel( int index )
        {
            TaskList.ElementAt( index).ToggleCancel();
        }

        public void PrintList()
        {
            PrintList(TaskList);
        }        

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

        public static void PrintList(List<TodoTask> tasks)
        {
        //  
        //     Id     Status          Description       Project          Due date              
        //     ==     ======          ===========       =======          ========
        //      1     [Pending]       Celebrate Xmas    Project Xmas     2024-12-24
        //      2     [Completed]     Walk the dog      Project Dog      2024-10-10
        //      3     [Completed]     Sing a song       Project X        2024-06-01
        //      .
        //      .
        //     10     [Cancelled]     Learn to fly      Project X        2025-11-23
        //
            foreach( var task in tasks)
            {

            }
        }



        public List<TodoTask> ToSortedList()
        {
            return TaskList.OrderBy( x => x.Cancelled )
                           .ThenBy( x => x.Status )
                           .ThenBy( x => DateOnly.Parse( x.DueDate ) )
                           .ToList();
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