using System.Diagnostics;
using static System.Console;

namespace ToDoList
{

    public class Menu
    {
        // flag to tell if we stay in ui loop or not
        private bool loop = false; 
        private IListControl _listControl; 
        
        public Menu( IListControl listControl )
        {
            _listControl = listControl;
        }

        private bool New()
        {
            WriteLine("New Task");
            // Ask user for Description, 
            // Ask user for Project
            // Ask user for Due Date
            // Validate date
            // Call TodoListControl.Add
            _listControl.Add( "hej", "hej", "hej");
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
            _listControl.Delete( index );
            return true;
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

        public bool Run()
        { 
            loop = true; 

            WriteLine
            (
                @"Menu:

                (N)ew Task { Enter Description, Project, Due Date, set to Pending }
                (E)dit { Pick Id } { Description, Project, Due Date }
                (D)elete Task
                (S)et Task Status to Completed / Pending
                (C)ancel Task
                (Q)uit"
            ); 
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