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
 

    public class TodoListApp
    {

        /*
            Main Program entry point
        */
        private static void Main(string[] args)
        {
            WriteLine();
            WriteLine( "Welcome to TODO List App 1.0" );
            WriteLine( "----------------------------" );
            WriteLine( "Today's date: " + DateTime.Today.ToString("yyyy-MM-dd") );
            WriteLine();

            var todoListControl = new TodoListControl();

            todoListControl.TaskList.AddRange
            ( 
                [
                   // new( "Oj", "Oj", "Oj" ), // string with intentionally invalid date
                    new( "Hej den här texten kan vara 40 tkn", "2023-10-01", "Project X", TaskStatus.Completed ),
                    new( "En text här",  "2024-09-02", "Project Y", TaskStatus.Completed ),
                    new( "Ja jaja ajajajajaj jajajjajaa jaja", "2021-11-03", "Project Z", TaskStatus.Pending ),
                    new( "Something I haven't done in time", "2023-11-03", "Project Z", TaskStatus.Pending ),
                    new( "This is next month", "2024-11-16", "Project ZZZ"),
                    new( "This is far into the future", "2029-03-17", "Project 3000"),
                    new( "Something I'm supposed to do today", DateTime.Today.ToString("yyyy-MM-dd"), "Project Z", TaskStatus.Pending ),
                    new( "Hey there, this is a descripton text", "2025-01-01", "Project Z1", TaskStatus.Pending, true ),
                    new( "Something I did not want to do", "2021-01-01", "Project Z1", TaskStatus.Pending, true )
                ]
            );


            // try 
            // { 
            //     // FormatException if DueDate contains garbage
            //     List<TodoTask> sortedTaskList = todoListControl.ToSortedList();
                
            //     todoListControl.TaskList = sortedTaskList;

            //     WriteLine(todoListControl.ToYamlString());
              
            // }
            // catch( System.FormatException e )
            // {
            //     WriteLine( "Invalid date field: " + e.Message );
            // }
            
            IListControl listControl = todoListControl;
            var menu = new Menu( listControl );
            while( menu.Run() )
            {
                // MainMenu loop
            }

            // WriteLine("----test code-----");

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
           
            // WriteLine("----end test code----");
        }

    }
}

