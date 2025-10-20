public class CommandHandlerResolver
{
    private readonly string _listsPath;

    public CommandHandlerResolver(string listsPath)
    {
        _listsPath = listsPath;
    }
    public ICommandHandler Resolve(string commandName)
    {
        return commandName.ToLower() switch
        {
            "add" => new AddHandler(_listsPath),
            "remove" => new RemoveHandler(_listsPath),
            "complete" => new CompleteHandler(_listsPath),
            "uncomplete" => new UncompleteHandler(_listsPath),
            "tasks" => new TasksHandler(_listsPath),
            "lists" => new ListsHandler(_listsPath),
            "create" => new CreateListHandler(_listsPath),
            "delete" => new DeleteListHandler(_listsPath),
            "open" => new OpenListHandler(_listsPath),
            "help" => new HelpHandler(_listsPath),
            _ => new UnknownHandler()
        };
    }
}