Console.WriteLine("Welcome To Your To Do List");
Console.WriteLine("--------------------------");
Console.WriteLine("Type 'help' to see available commands.");

var running = true;
var listsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Todo Lists");
var ListManager = new ListManager(listsPath);

var resolver = new CommandHandlerResolver(ListManager);

while (running)
{
    Console.Write("\nEnter Command: ");
    var input = Console.ReadLine()?.Trim();
    
    var parts = input?.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    var command = parts?.Length > 0 ? parts[0].ToLower() : "";
    var arguments = parts?.Length > 1 ? parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();

    if (command == "exit")
    {
        Console.WriteLine("Exiting the application. Goodbye!");
        running = false;
        continue;
    }

    var handler = resolver.Resolve(command);
    handler.Handle(arguments);
}