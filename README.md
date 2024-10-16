# ToDoList application
## C# training project

### Description

A basic todo list console application, with functionalty to track tasks to do in a list.
Tasks in the todo list can be sorted by status (Done or not, or cancelled), and Due Date. 

The application main menu:
```
(N)ew,           Create a new task to the list and add it
(E)dit,          Edit properties of an existing task
(D)elete,        Delete a task
(S)tatus change  Change status of the task between Pending and Completed
(C)ancel         Cancel the task
(Q)uit           Quit the app
```

### Pre-requisites

YamlDotNet package: 
https://github.com/aaubry/YamlDotNet

CLI installation:

```
dotnet add package YamlDotNet --version 16.1.3
```

### Good to know

Writes to file todolist.yaml

### Implementation

Implemented in a Model View Controller (MVC) pattern, with classes for tasks, communication between
ui view and the code that controls the model through a C# interface.

<img src="https://github.com/ppettert/ToDoList/blob/main/doc/Diagram.svg" width="75%" height="75%">

### TODO

There is much room for improvements. Focus was to finish the core functionality in time for presentation.

It would be much nicer if I could navigate in the list, instead of entering line number. 
The separation between visual presentation and data makes it easy to adapt the code to a GUI or web app, when time permits.

