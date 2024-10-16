# ToDoList application
## C# training project

### Description

A basic todo list console application, with functionalty to track tasks to do in a list.
Tasks in the todo list can be sorted by status (Done or not, or cancelled), and Due Date. 

Implemented in a Model View Controller (MVC) pattern, with classes for tasks, communication between
ui view and the code that controls the model through a C# interface.

### Pre-requisites

YamlDotNet package: 
https://github.com/aaubry/YamlDotNet

CLI installation:

```
dotnet add package YamlDotNet --version 16.1.3
```

### Good to know

Writes to file todolist.yaml

### TODO

There is much room for improvements. Focus was to finish the core functionality in time.

It would be much nicer if I could navigate in the list, instead of entering line number.

