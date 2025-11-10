public class CommandHandlerResolver
{
    private ListManager? _listManager;

    public CommandHandlerResolver() { }

    public CommandHandlerResolver(ListManager listManager)
    {
        _listManager = listManager;
    }

    public ICommandHandler? ResolveProjectCommand(string commandName)
    {
            if (_listManager is null)
        {
            return null;
        }
            return commandName.ToLower() switch
        {
            "add" => new AddHandler(_listManager),
            "remove" => new RemoveHandler(_listManager),
            "complete" => new CompleteHandler(_listManager),
            "uncomplete" => new UncompleteHandler(_listManager),
            "tasks" => new TasksHandler(_listManager),
            "lists" => new ListsHandler(_listManager),
            "create" => new CreateListHandler(_listManager),
            "delete" => new DeleteListHandler(_listManager),
            "open" => new OpenListHandler(_listManager),
            "migrate" => new MigrateHandler(_listManager),
            _ => new UnknownHandler()
        };
    }

    public ICommandHandler? ResolveGlobalCommand(string commandName)
    {
        return commandName.ToLower() switch
        {
            "init" => new InitHandler(),
            "help" => new HelpHandler(),
            _ => null
        };
    }
}