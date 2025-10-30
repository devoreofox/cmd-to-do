using System.Text.Json;

public class ListManager
{
    private readonly string _listsPath;
    private readonly string _flagFilePath;
    private string? _activeList;

    public ListManager(string listsPath)
    {
        _listsPath = listsPath;
        _flagFilePath = Path.Combine(_listsPath, ".activeList.flag");
        Directory.CreateDirectory(_listsPath);

        LoadActiveList();
    }

    public string GetPath()
    {
        return _listsPath;
    }

    public string? GetActiveList()
    {
        if (_activeList == null)
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
        SaveActiveList();
    }

    public string GetFilePath()
    {
        if (_activeList == null)
        {
            throw new ListNotFoundException("No active list selected.");
        }
        return Path.Combine(_listsPath, $"{_activeList}.json");
    }

    public List<string> GetLists()
    {
        if (!Directory.Exists(_listsPath))
        {
            Directory.CreateDirectory(_listsPath);
        }
        var files = Directory.GetFiles(_listsPath, "*.json");
        return files.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
    }

    public bool ListExists(string listName)
    {
        string filePath = Path.Combine(_listsPath, $"{listName}.json");
        return File.Exists(filePath);
    }

    public void CreateList(string listName)
    {
        foreach (char c in Path.GetInvalidFileNameChars()) listName = listName.Replace(c, '_');
        string filePath = Path.Combine(_listsPath, $"{listName}.json");
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
        string filePath = Path.Combine(_listsPath, $"{listName}.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            if (_activeList == listName)
            {
                _activeList = null;
                SaveActiveList();
            }
        }
        else
        {
            throw new ListNotFoundException($"List '{listName}' does not exist.");
        }
    }

    public List<TodoItem> LoadTasks()
    {
        if (_activeList == null) throw new ListNotFoundException("No active list selected.");
        string json = File.ReadAllText(GetFilePath());
        if (string.IsNullOrWhiteSpace(json)) return new List<TodoItem>();
        return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
    }

    public void SaveTasks(List<TodoItem> tasks)
    {
        if (_activeList == null) throw new ListNotFoundException("No active list selected.");
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(GetFilePath(), json);
    }

    private void LoadActiveList()
    {
        if (File.Exists(_flagFilePath))
        {
            var savedListName = File.ReadAllText(_flagFilePath).Trim();

            if (!string.IsNullOrEmpty(savedListName) && ListExists(savedListName)) _activeList = savedListName;
            else
            {
                _activeList = null;
                File.Delete(_flagFilePath);
            } 
        }
    }

    public void SaveActiveList()
    {
        if (!string.IsNullOrEmpty(_activeList))
        {
            File.WriteAllText(_flagFilePath, _activeList);
        }
        else if (File.Exists(_flagFilePath))
        {
            File.Delete(_flagFilePath);
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