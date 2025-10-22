public class HelpHandler : ICommandHandler
{
    public HelpHandler() { }
    public void Handle(string[] args)
    {
        Console.WriteLine("Available Commands:");
        Console.WriteLine("-------------------");
        Console.WriteLine("help - Displays this help message.");
        Console.WriteLine("lists - Displays all to-do lists.");
        Console.WriteLine("create [name] - Creates a new to-do list.");
        Console.WriteLine("delete [name] - Deletes an existing to-do list.");
        Console.WriteLine("open [name] - Opens an existing to-do list.");
        Console.WriteLine("tasks [complete | incomplete] - Lists tasks in the active to-do list with optional filtering.");
        Console.WriteLine("add [task] - Adds a new task to the list.");
        Console.WriteLine("remove [task number | all | complete | incomplete] - removes tasks matching the given entry");
        Console.WriteLine("complete [task number] - Marks a task as completed.");
        Console.WriteLine("uncomplete [task number] - Marks a task as incomplete.");
        Console.WriteLine("exit - Exits the application.");
    }
}