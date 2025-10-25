using System.Text.Json;

public static class MigrationHelper
{
    private const string MigrationFlagFile = "migration_complete.flag";

    public static void MigrateToJson(string listsPath)
    {
        string flagFilePath = Path.Combine(listsPath, MigrationFlagFile);
        var txtFiles = Directory.GetFiles(listsPath, "*.txt");

        if(File.Exists(flagFilePath))
        {
            return;
        }

        if (txtFiles.Length == 0)
        {
            File.WriteAllText(flagFilePath, "No legacy text lists found. Migration not required.");
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
        foreach (var txtFile in txtFiles)
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
                Console.WriteLine($"Failed to migrate '{Path.GetFileName(txtFile)}': {ex.Message}");
            }
        }

        Console.WriteLine("Migration completed.");
        File.WriteAllText(flagFilePath, "Migration completed on " + DateTime.Now);
        DeleteLegacy(listsPath);
    }

    private static void DeleteLegacy(string listsPath)
    {
        var legacyFiles = Directory.GetFiles(listsPath, "*.txt");
        if (legacyFiles.Length == 0) return;

        Console.WriteLine();
        Console.WriteLine("Legacy text lists remain after migration.");
        Console.WriteLine("Would you like to delete the old .txt files now? (y/n)");
        string? input = Console.ReadLine()?.Trim().ToLower();

        if (input == "y" || input == "yes")
        {
            foreach (var file in legacyFiles)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Deleted: {Path.GetFileName(file)}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Failed to delete {Path.GetFileName(file)}: {ex.Message}");
                }
            }
            Console.WriteLine("All legacy .txt files deleted.");
        }
        else
        {
            Console.WriteLine("Legacy .txt files retained.");
        }
    }
}