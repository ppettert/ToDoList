namespace ToDoList
{

    // This class is needed by Yaml (de)serializer/parser   
    // And also acts as controller of the model which is
    // the contained TaskList and it's TodoTask items.
    public class TodoListControl
    {
        public List<TodoTask>? TaskList { get; set; }

        // public TodoListControl()
        // {
        // }

        // All List/Task actions such as Add, Edit & Toggle, List, 
        //  will be passing through here
    }
}