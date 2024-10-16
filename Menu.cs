
using System.Linq.Expressions;
using static System.Console;

namespace ToDoList
{

    public class Menu(IListControl listControl)
    {
        // flag to tell if we stay in ui loop or not
        private bool loop = false; 
        private IListControl _listControl = listControl;

        /* returns true if action was performed */
        private bool New()
        {
            WriteLine("New Task");
            // Ask user for Description, 
            // Ask user for Project
            // Ask user for Due Date
            // Validate date
            // Call TodoListControl.Add
            _listControl.Add( "I have to do something", "2011-11-11", "Project Q");
            return true;
        }

        /* returns true if action was performed */
        private bool Edit()
        {
            WriteLine("Edit Task");
            // Ask User for Nbr in List
            // Validate
            // Ask user for Property (Description, Project, Due Date)
            // Ask user for Input
            // If Property is Due Date validate
            // Call TodoListControl.Edit(Nbr,PropertyName,Input)
            int index = 0;

            _listControl.Edit( index, "edited", "project" );
            return true;
        }

        /* returns true if action was performed */
        private bool Delete()
        {
            WriteLine("Delete Task");
            // Ask User for Nbr in List
            // Validate, Confirm
            // Call TodoListControl.Delete(Nbr)
            int index = 0;
            try 
            {
                _listControl.Delete( index );
                return true;
            }
            catch( System.ArgumentOutOfRangeException e )
            {
                WriteLine( "Did not delete: " + e.Message );
                return false;
            }
            
        }

        /* returns true if action was performed */
        private bool SetStatus()
        {
            WriteLine("Set Task Status");    
            // Ask User for Nbr in List
            // Call TodoListControl.ToggleStatus(Nbr)   
            int index = 0;  
            _listControl.SetStatus( index );
            return true;
        }

        /* returns true if action was performed */
        private bool Cancel()
        {
            WriteLine("Cancel task");
            // Ask User for Nbr in List
            // Call TodoListControl.SetStatus(Cancelled)
            int index = 0;
            _listControl.Cancel( index );
            return true;
        }

        private bool Quit()
        {
            WriteLine("Bye.");
            return true;         
        }                

        /* Prints a sorted list in rows and colums with headers */
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

            Write("Id".PadRight(5));
            Write("Status".PadRight(15));
            Write("Description".PadRight(40));
            Write("Project".PadRight(20));
            Write("Due date");
            WriteLine();

            Write("==".PadRight(5));
            Write("=======".PadRight(15));
            Write("===========".PadRight(40));
            Write("=======".PadRight(20));
            Write("========");
            WriteLine();

            if( tasks.Count == 0 )
            {
                WriteLine();
                WriteLine("Empty List!");
                WriteLine();
                return;
            }

            int index = 1;
            foreach( var task in tasks)
            {
                Write($"{index}".PadRight(5));
                
                if( task.Cancelled )
                {
                    ForegroundColor = ConsoleColor.White; // Looks like grey in my terminal
                    Write("Cancelled".PadRight(15));
                }
                else
                {
                    if( task.Status == TaskStatus.Pending )
                    {
                        if( DateOnly.TryParse( task.DueDate, out DateOnly dueDate ))
                        {
                            switch( dueDate.CompareTo( DateOnly.FromDateTime( DateTime.Today )) )
                            {
                                case 0: // today
                                    ForegroundColor = ConsoleColor.Red;
                                    break;
                                case -1:
                                    ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                default:
                                    ForegroundColor = ConsoleColor.Green;
                                    break;
                            };

                        }

                    }
                    Write($"{task.Status}".PadRight(15));
                }

                Write($"{task.Description}".PadRight(40));
                Write($"{task.Project}".PadRight(20));
                Write($"{task.DueDate}");
                ResetColor();
                WriteLine();      
                index++;          

            }
        }

        public bool Run()
        { 
            loop = true; 

            PrintList(_listControl.ToSortedList());
            WriteLine();

            WriteLine("Menu: (N)ew, (E)dit, (D)elete, (S)tatus change, (C)ancel, (Q)uit");
            WriteLine();

            string? input = ReadLine()?.Trim();

            switch( input )
            {
                case "N":
                    New();
                    break;
                case "E":
                    Edit();
                    break;
                case "D":
                    Delete();
                    break;
                case "S":
                    SetStatus();
                    break;
                case "C":
                    Cancel();
                    break;
                case "Q":
                    Quit();
                    loop = false;
                    break;
                default:
                    WriteLine("No.");
                    break;

            };

            return loop;

        }
    }
}