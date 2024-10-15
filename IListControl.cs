namespace ToDoList
{
   public interface IListControl
   {
        public void Add( string description, string dueDate, string project );
        public void Edit( int index, string propertyString, string propertyName );
        public void Delete( int index );
        public void SetStatus( int index );
        public void Cancel( int index );
        public void PrintList();
   }
} 
