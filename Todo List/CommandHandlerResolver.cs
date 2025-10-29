public class CommandHandlerResolver
{
    private readonly ListManager _listManager;

    public CommandHandlerResolver(ListManager listManager)
    {
        _listManager = listManager;
    }
    public ICommandHandler Resolve(string commandName)
    {
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
            "help" => new HelpHandler(),
            _ => new UnknownHandler()
        };
    }
}