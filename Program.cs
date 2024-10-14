using static System.Console;
using System;
using System.Collections.Generic;
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
    public enum TaskStatus
    {
        Pending,
        Completed
    }

    public class TodoTask
    {
      //   private DateOnly _dueDate = dueDate;

        public string? TaskTitle { get; set; } 
        public string? DueDate { get; set; } 
        public TaskStatus Status { get; set; } 
        public string? Project { get; set; } 

        public TodoTask()
        {
           // Needed by yaml deserialize 
        }
        public TodoTask( string taskTitle, string dueDate, TaskStatus status, string project ) 
        {
            TaskTitle = taskTitle;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }
    }

    public class TodoList 
    {
        public List<TodoTask>? TaskList { get; set; }
    }


    public class Program
    {
        
        /*
            Main Program entry point
        */
        private static void Main(string[] args)
        {
            WriteLine("Hello world!\n-----------");

            var stufftodo = new TodoList
            {
                TaskList =
                [
                    new( "Hej", "2023-10-01", TaskStatus.Completed, "Project X" ),    
                    new( "På", "2024-09-02", TaskStatus.Completed, "Project Y" ),
                    new( "Dig", "2025-11-03", TaskStatus.Pending, "Project Z" )
                ]
            };
            
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(stufftodo);
            WriteLine(yaml);

            File.WriteAllText("todolist.yaml", yaml);

            /*

                turn around

            */

            var yammy = File.ReadAllText( "todolist.yaml" );

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

           
          //  ToDoList stufftodo2 = 
            var mystuffs = deserializer.Deserialize<TodoList>(yammy);


/**
            var p = deserializer.Deserialize<Person>(yml);
            var h = p.Addresses["home"];
            System.Console.WriteLine($"{p.Name} is {p.Age} years old and lives at {h.Street} in {h.City}, {h.State}.");
**/

            WriteLine("----\ngbye");
        }

    }
}

