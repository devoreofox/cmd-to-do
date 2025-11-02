public class CommandHandlerResolver
{
    private TodoProject? _todoProject;
    private ListManager? _listManager;

    public ICommandHandler Resolve(string commandName)
    {

        string cmd = commandName.ToLower();

        bool isGlobalCommand = cmd == "init" || cmd == "help";

        if (!isGlobalCommand)
        {
            if (!TodoProject.IsInitialized())
            {
                throw new NotInitializedException("No .todo directory found, please use `todo init` to initialize the project.");
            }
            _todoProject ??= new TodoProject();
            _listManager ??= new ListManager(_todoProject);
        }

            return cmd switch
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
            "init" => new InitHandler(),
            "help" => new HelpHandler(),
            _ => new UnknownHandler()
        };
    }
}