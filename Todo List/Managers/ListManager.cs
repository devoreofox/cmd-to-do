public class ListManager
{
    private readonly string _listsPath;
    private string? _activeList;

    public ListManager(string listsPath)
    {
        _listsPath = listsPath;
        Directory.CreateDirectory(_listsPath);
    }

    public string? GetActiveList()
    {
        if(_activeList == null)
        {
            throw new ListNotFoundException("No active list selected.");
        }
        return _activeList;
    }

    public void SetActiveList(string listName)
    {
        if (!ListExists(listName))
        {
            throw new ListNotFoundException($"List '{listName}' does not exist.");
        }
        _activeList = listName;
    }

    public string GetFilePath()
    {
        if (_activeList == null)
        {
            throw new ListNotFoundException("No active list selected.");
        }
        return Path.Combine(_listsPath, $"{_activeList}.txt");
    }

    public List<string> GetLists()
    {
        if (!Directory.Exists(_listsPath))
        {
            Directory.CreateDirectory(_listsPath);
        }
        var files = Directory.GetFiles(_listsPath, "*.txt");
        return files.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
    }

    public bool ListExists(string listName)
    {
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");
        return File.Exists(filePath);
    }

    public void CreateList(string listName)
    {
        foreach (char c in Path.GetInvalidFileNameChars()) listName = listName.Replace(c, '_');
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        else
        {
            throw new ListAlreadyExistsException($"List '{listName}' already exists.");
        }
    }

    public void DeleteList(string listName)
    {
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            if(_activeList == listName)
            {
                _activeList = null;
            }
        }
        else
        {
            throw new ListNotFoundException($"List '{listName}' does not exist.");
        }
    }
}

public class ListNotFoundException : Exception
{
    public ListNotFoundException(string message) : base(message) { }
}

public class ListAlreadyExistsException : Exception
{
    public ListAlreadyExistsException(string message) : base(message) { }
}