using static System.Console;

namespace ToDoList
{

    public class Menu
    {
        // flag to tell if we stay in ui loop or not
        private bool loop = false; 
        public Menu()
        {
        }

        private bool New()
        {
            WriteLine("New Task");
            return true;
        }

        private bool Edit()
        {
            WriteLine("Edit Task");
            return true;
        }

        private bool Delete()
        {
            WriteLine("Delete Task");
            return true;
        }

        private bool SetStatus()
        {
            WriteLine("Set Task Status");         
            return true;
        }

        private bool Cancel()
        {
            WriteLine("Cancel task");
            return true;
        }

        private bool Quit()
        {
            WriteLine("Bye.");
            return false;         
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
                    return New();
                case "E":
                    return Edit();
                case "D":
                    return Delete();
                case "S":
                    return SetStatus();
                case "C":
                    return Cancel();
                case "Q":
                    loop = false;
                    WriteLine("Goodbye.");
                    break;
                default:
                    WriteLine("No.");
                    break;

            };

            return loop;

        }
    }
}