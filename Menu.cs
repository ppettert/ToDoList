
using static System.StringComparison;
using static System.Console;
using YamlDotNet.Serialization.NodeTypeResolvers;

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
            string? description = "";
            string? project = "";
            string? dueDate = "";

            WriteLine();
            WriteLine("Add New Task, enter description, project name and due date.");

            bool done = false; 
            while( !done )
            {
                Write("Description: ");
                description = ReadLine()?.Trim();
                description ??= ""; // if null set it to ""

                if( description != "" )
                {
                    if( description.Equals("Q", OrdinalIgnoreCase ))
                    {
                        WriteLine("Aborted!");
                        WriteLine();
                        return false; 
                    }
                    done = true;
                }
            }

            done = false;
            while( !done )
            {
                Write("Project: ");
                project = ReadLine()?.Trim();
                project ??= ""; // if null set it to ""

                if( project != "" )
                {
                    if( project.Equals("Q", OrdinalIgnoreCase ))
                    {
                        WriteLine("Aborted!");
                        WriteLine();
                        return false; 
                    }

                    done = true;
                }
            }
            
            done = false;
            while( !done )
            {
                Write("Due Date YYYY-MM-DD: ");
                dueDate = ReadLine()?.Trim();
                dueDate ??= ""; // if null set it to ""

                if( dueDate != "" )
                {
                    if( dueDate.Equals("Q", OrdinalIgnoreCase ))
                    {
                        WriteLine("Aborted!");
                        WriteLine();
                        return false; 
                    }

                    done = DateOnly.TryParse( dueDate, out DateOnly dueDateResult );
                    if( !done ) 
                    {
                        WriteLine($"{dueDate} is not a valid date");
                    }
                }
            }           

            _listControl.Add( description, dueDate, project );
            return true;
        }

        /* 
            asks for user line of task to delete/cancel/edit/change status
            returns real index of item on line
            user is presented with a list of sorted items, 
            they are not sorted unless presented
        */
        private int LineOpHelper( List<TodoTask> sortedList, string message )
        {
            bool done = false; 
            while( !done ) 
            {
                Write(message);

                var input = ReadLine()?.Trim();

                input ??= ""; // if null set it to empty string

                if( input.Equals( "Q", OrdinalIgnoreCase ) )
                {
                    WriteLine("Aborted!");
                    WriteLine();
                    return -1; 
                }
            
                if( int.TryParse( input,  out int index ) )
                {
                    // check range
                    if( index > 0 && index <= _listControl.Count() )
                    {
                        var task = sortedList.ElementAt(index-1);
                        return _listControl.IndexOf( task );
                    }
                    else
                    {
                        WriteLine("Index out of range.");    
                    }
                }
                else
                {
                    WriteLine("Invalid line number.");
                }
            
            }  
            return -1;               
        }

        /* returns true if action was performed */
        private bool Delete( List<TodoTask> sortedList )
        {
            WriteLine();
            WriteLine("Delete task.");
            
            bool done = false; 
            while( !done ) 
            {
                var index = LineOpHelper( sortedList, "Enter line number for task to delete: " );

                if( index < 0 )
                {
                    // User entered Q 
                    return false; 
                }

                // WriteLine($"To delete: {index}");

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
            
            return false; 
            
        }

        /* returns true if action was performed */
        private bool Edit( List<TodoTask> sortedList )
        {
            WriteLine("Edit Task.");

            // Ask user for Property (Description, Project, Due Date)
            // Ask user for Input
            // If Property is Due Date validate
            // Call TodoListControl.Edit(Nbr,PropertyName,Input)
            var index = LineOpHelper( sortedList, "Enter line number for task to edit: " );
                 
            if( index < 0 )
            {
                // User entered Q 
                return false; 
            }           

            // WriteLine($"editing item {index}");

            bool done = false; 
            while( !done )
            {
                Write("Enter property to edit, (D)escription, (P)roject, Da(T)e: ");
                var input = ReadLine()?.Trim().ToLower();
                input ??= "";

                if( input.Equals( "q" ))
                {
                    WriteLine("Aborted!");
                    WriteLine();
                    return false; 
                }

                if( input.Equals( "d" ) || input.Equals( "p" ) )
                {
                    var propertyString = "";
                    if( input.Equals("d") ) input = "description";
                    if( input.Equals("p") ) input = "project";
                    
                    while( !done )
                    {

                        Write($"Enter {input}: ");
                        propertyString = ReadLine()?.Trim();
                        if( propertyString != "" )
                        {
                            done = true;
                        }
                    }
                    
                    if( propertyString is not null )
                    {
                        WriteLine();
                        _listControl.Edit( index, propertyString, input );                    
                        return true; 
                    }
                    else
                    {
                       done = false;  
                    }
                }
                else if( input.Equals( "t" ) )
                {
                    var dueDateString = "";
                    done = false;
                    while( !done )
                    {
                        Write($"Enter date (YYYY-MM-DD): ");
                        dueDateString = ReadLine()?.Trim();
                        if( dueDateString != "" )
                        {
                            // Validate that dueDateString is a parsable date
                            if( DateOnly.TryParse( dueDateString, out var dontCare ) )
                            {
                                done = true; 
                            }
                        }
                    }

                    if( dueDateString is not null )
                    {
                        WriteLine();
                        _listControl.Edit( index, dueDateString, "duedate" );
                        return true; 
                    }
                    else 
                    {
                        done = false; 
                    }
                } 
                else 
                {
                    WriteLine("Invalid property.");
                }      

            }

  
            return false;
        }


        /* returns true if action was performed */
        private bool SetStatus( List<TodoTask> sortedList )
        {
            WriteLine("Set Task Status.");    
            var index = LineOpHelper( sortedList, "Enter line number for task to change status: " );
              
            if( index < 0 )
            {
                // User entered Q 
                return false; 
            }   

            if( _listControl.SetStatus( index ) )
            {
                return true;
            }
            else 
            {
                WriteLine("Cannot change status on cancelled items!");
                WriteLine();
                return false; 
            }
        }

        /* returns true if action was performed */
        private bool Cancel( List<TodoTask> sortedList )
        {
            WriteLine("Cancel task");
            var index = LineOpHelper( sortedList, "Enter line number for task to (un)cancel: " );
            
            if( index < 0 )
            {
                // User entered Q 
                return false; 
            }   

            var cancelled = _listControl.Cancel( index );
            if( cancelled )
            {
                WriteLine("Task Cancelled!");
                WriteLine();
            }
            else
            {
                WriteLine("Task uncancelled!");
                WriteLine();
            }

            return true;
        }

        private bool Quit()
        {
            WriteLine("Goodbye!");
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

            Write("Row".PadRight(5));
            Write("Status".PadRight(15));
            Write("Description".PadRight(40));
            Write("Project".PadRight(20));
            Write("Due date");
            WriteLine();

            Write("===".PadRight(5));
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
                Write($"{index}".PadLeft(3).PadRight(5));
                
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

            var sortedList = _listControl.ToSortedList();

            PrintList( sortedList );
            WriteLine();

            WriteLine("Menu: (N)ew, (E)dit, (D)elete, (S)tatus change, (C)ancel/Uncancel, (Q)uit");
            WriteLine();

            string? input = ReadLine()?.Trim().ToUpper();

            switch( input )
            {
                case "N":
                    if( New() )
                        WriteLine();
                    {
                        WriteLine("New task added!");
                        // Write to file
                    }
                    break;
                case "E":
                    if( Edit( sortedList ) )
                    {
                        WriteLine("Task edited!");
                        WriteLine();
                        // Write to file
                    }
                    break;
                case "D":
                    if( Delete( sortedList ) )
                    {
                        WriteLine("Task deleted!");
                        WriteLine();
                        // Write to file
                    }
                    break;
                case "S":
                    if( SetStatus( sortedList ) )
                    {
                        WriteLine("Task status changed!");
                        WriteLine();
                        // Write to file
                    }
                    break;
                case "C":
                    if( Cancel( sortedList ) ) 
                    {
                        WriteLine("Task cancelled!");
                        WriteLine();                        
                        // Write to file
                    }
                    break;
                case "Q":
                    Quit();
                    loop = false;
                    break;
                default:
                    WriteLine("Menu choice not understood.");
                    break;

            };

            return loop;

        }
    }
}