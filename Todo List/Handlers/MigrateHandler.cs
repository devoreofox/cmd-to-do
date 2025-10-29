using System.Text.Json;

public class MigrateHandler : ICommandHandler
{
    private readonly ListManager _listManager;
    public MigrateHandler(ListManager listManager)
    {
        _listManager = listManager;
    }
    public void Handle(string[] args)
    {
        var listsPath = _listManager.GetPath();
        var txtFiles = Directory.GetFiles(listsPath, "*.txt");

        if (txtFiles.Length == 0)
        {
            Console.WriteLine("No legacy text-based todo lists found. Migration not required.");
            return;
        }

        Console.WriteLine("Legacy text-based todo lists detected.");

        foreach(var txtFile in txtFiles)
        {
            try
            {
                string jsonFile = Path.ChangeExtension(txtFile, ".json");

                if (File.Exists(jsonFile))
                {
                    Console.WriteLine($"Skipping migration for '{Path.GetFileName(txtFile)}' as JSON file already exists.");
                    continue;
                }

                Console.WriteLine($"Migrating '{Path.GetFileName(txtFile)}' to JSON format...");
                var lines = File.ReadAllLines(txtFile);
                var tasks = new List<TodoItem>();

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    bool isCompleted = line.StartsWith("[X] ");
                    string description = line.Substring(isCompleted ? 4 : 4).Trim();
                    var task = new TodoItem(description) { IsCompleted = isCompleted };
                    tasks.Add(task);
                }

                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFile, json);
                Console.WriteLine($"Migration completed for '{Path.GetFileName(txtFile)}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error migrating '{Path.GetFileName(txtFile)}': {ex.Message}");
            }
        }

        Console.WriteLine("All migrations completed. You may delete legacy files now.");
    }
}