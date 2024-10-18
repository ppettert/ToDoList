
namespace ToDoList
{
   /* 
    * Defines interface with methods to be implemented by list controls 
    * that can be accessed by Views such a text ui, GUI or web interface
    */
   public interface IListControl
   {
      /*
       * Create and add a new TodoTask to the list
       */
      public void Add(string description, string dueDate, string project);

      /*
       * Edits TodoTask at index, just passes the call to TodoTask class
       */ 
      public void Edit(int index, string propertyString, string propertyName);

      /* 
       * Deletes the task at the given list index 
       */
      public void Delete(int index);
      
      /*
       * Switches the status of the task at the list index 
       * Status defined by TaskStatus enum in TodoTask class
       * returns: bool if changed, false if there was no item at index
       */     
      public bool SetStatus(int index);
      
      /*
       * Cancel the task at the list index 
       * returns: bool if cancelled, false if there was no item at index
       */
      public bool Cancel(int index);
      
      /* 
       * returns the number of items in the list
       */ 
      public int Count();

      /* 
       * returns a sorted list with TodoTasks from the internal list representation 
       */
      public List<TodoTask> ToSortedList();
      
      /* 
       * returns the actual index of a task in the internal list representation,
       * which differs from the visual presentation  
       */
      public int IndexOf(TodoTask task);
      
      /*
       * returns a yaml-formatted string representation of the internal ToDoTask list
       */
      public string ToYamlString();
   }
}
