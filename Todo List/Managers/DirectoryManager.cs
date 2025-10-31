public class DirectoryManager
{
    private readonly string _rootPath;
    private readonly string _listsPath;
    private readonly string _flagPath;

    public DirectoryManager()
    {
        _rootPath = FindRoot() ?? throw new NotInitializedException("No .todo directory found, please use `todo init` to initialize the project.");
        _listsPath = Path.Combine(_rootPath, "lists");
        _flagPath = Path.Combine(_rootPath, ".activeList.flag");

       
    }

    public string GetRootPath()
    {
        return _rootPath;
    }

    public string GetListsPath()
    {
        return _listsPath;
    }

    public string GetFlagPath()
    {
        return _flagPath;
    }



    private static string? FindRoot()
    {
        var currentDir = Directory.GetCurrentDirectory();

        while (true)
        {
            var path = Path.Combine(currentDir, ".todo");
            if (Directory.Exists(path))
            {
                return path;
            }

            var parentDir = Directory.GetParent(currentDir);
            if (parentDir == null)
            {
                return null;
            }
            currentDir = parentDir.FullName;
        }
    }

    public static bool IsInitialized()
    {
        if (FindRoot() == null)
        {
            return false;
        }

        return true;
    }

    public static void Initialize()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var todoDir = Path.Combine(currentDir, ".todo");

        if (Directory.Exists(todoDir))
        {
            throw new AlreadyInitializedException(".todo directory already exists.");
        }
        Directory.CreateDirectory(todoDir);
        Directory.CreateDirectory(Path.Combine(todoDir, "lists"));
        File.Create(Path.Combine(todoDir, ".activeList.flag")).Dispose();
    }
}

public class NotInitializedException : Exception
{
    public NotInitializedException(string message) : base(message) { }
}

public class AlreadyInitializedException : Exception
{
    public AlreadyInitializedException(string message) : base(message) { }
}
