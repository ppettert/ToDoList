using static System.Console;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace ToDoList
{

    /*
        You can think like this about save to file and read from file:  
        For example ToDoList Project 
        
        Each task includes the following properties: 

        - Task title: A short description of the task. 
        - Due date: The date by which the task should be completed. 
        - Status: Indicates whether the task is completed or pending. 
        - Project: Specifies the project to which the task belongs. 
        
        You can think like this; 
        
        1. Get Info from User (Also you can added already some info into the file) 
        2. Create your objects 
        3. Add them to the List 
        4. You can show, sort, update them. 
        5. Send(Write) to file (you can use just txt file or any other format) 
        
        6. Think ==> It will write to file Each object (title, date, project name and status) will be added to a separate line in
        the txt file.  
        
        7. Think ==> When you read data from File, it will read line by line and each line will create again object and will be
        added in to the list( or array)  

        You can sort it when you get the info again from file. Just think and create your appropriate functions and methods. 
    */
 

    public class Program
    {

        /*
            Main Program entry point
        */
        private static void Main(string[] args)
        {
            // WriteLine("Hello world!\n-----------");

            var todoListControl = new TodoListControl
            {
                TaskList = []
            };

            todoListControl.TaskList.AddRange
            ( 
                [
                   // new( "Oj", "Oj", "Oj" ), // intentional string with invalid date
                    new( "Hej", "2023-10-01", "Project X", TaskStatus.Completed ),
                    new( "På",  "2024-09-02", "Project Y", TaskStatus.Completed ),
                    new( "Dig", "2021-11-03", "Project Z", TaskStatus.Pending ),
                    new( "Ooo", "2025-01-01", "Project Z1", TaskStatus.Pending, true )
                ]
            );


            try 
            { 
                // FormatException if DueDate contains garbage
                List<TodoTask> sortedTasks3 = todoListControl.TaskList
                                                             .OrderBy( x => x.Cancelled )
                                                             .ThenBy( x => x.Status )
                                                             .ThenBy( x => DateOnly.Parse( x.DueDate ) )
                                                             .ToList();
                
                todoListControl.TaskList = sortedTasks3; 

                WriteLine(todoListControl.ToYamlString());
              
            }
            catch( System.FormatException e )
            {
                WriteLine( "Invalid date field: " + e.Message );
            }
            
            IListControl listControl = todoListControl;
            var menu = new Menu( listControl );
            while( menu.Run() )
            {
                // MainMenu loop
            }

            var yaml = todoListControl.ToYamlString();

            File.WriteAllText("todolist.yaml", yaml);

            /*

                turn around

            */

            var yaml_from_file = File.ReadAllText( "todolist.yaml" );

            // Sanity Test cases
            // ctrl_a, ctrl_b and ctrl_c will contain the same data if successful
            var ctrl_a = TodoListControl.FromYamlString( yaml );
            var ctrl_b = TodoListControl.FromYamlString( yaml_from_file );     

            var ctrl_c = new TodoListControl( yaml );
           
            WriteLine("----\ngbye");
        }

    }
}

