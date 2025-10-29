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
        Console.WriteLine("Would you like to migrate them to the new JSON format? (y/n)");
        var input = Console.ReadLine()?.Trim().ToLower();

        if (input != "y" && input != "yes")
        {
            Console.WriteLine("Migration skipped. Legacy text lists will remain unchanged.");
            return;
        }

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

        Console.WriteLine("All migrations completed.");

        Console.WriteLine("Would you like to delete the legacy text files? (y/n)");
        input = Console.ReadLine()?.Trim().ToLower();
        if (input == "y" || input == "yes")
        {
            foreach (var txtFile in txtFiles)
            {
                try
                {
                    File.Delete(txtFile);
                    Console.WriteLine($"Deleted legacy file '{Path.GetFileName(txtFile)}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete '{Path.GetFileName(txtFile)}': {ex.Message}");
                }
            }
            Console.WriteLine("Legacy text files deleted.");
        }
        else
        {
            Console.WriteLine("Legacy text files retained.");
        }
    }
}