using System;
using System.IO;

Console.WriteLine("Welcome To Your To Do List");
Console.WriteLine("--------------------------");
Console.WriteLine("Type 'help' to see available commands.");

var running = true;
var listsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Todo Lists");
Directory.CreateDirectory(listsPath);

string? activeList = null;


while (running)
{
    Console.Write("\nEnter command: ");
    HandleCommand();
}

void HandleCommand()
{
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Please enter a command.");
        return;
    }


    var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    var command = parts[0].ToLower();
    var argument = parts.Length > 1 ? parts[1] : string.Empty;

    switch (command)
    {
        case "help":
            ShowHelp();
            break;
        case "exit":
            Console.WriteLine("Exiting the application. Goodbye!");
            running = false;
            break;
        case "create":
            CreateList(argument);
            break;
        case "delete":
            DeleteList(argument);
            break;
        case "lists":
            Lists();
            break;
        case "open":
            OpenList(argument);
            break;
        case "add":
            AddTask(argument);
            break;
        case "tasks":
            ListTasks();
            break;
        case "remove":
            RemoveTask(int.Parse(argument));
            break;
        case "complete":
            CompleteTask(int.Parse(argument));
            break;
        case "uncomplete":
            UncompleteTask(int.Parse(argument));
            break;
        default:
            Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
            break;
    }
}

static void ShowHelp()
{
    Console.WriteLine("Available Commands:");
    Console.WriteLine("-------------------");
    Console.WriteLine("help - Displays this help message.");
    Console.WriteLine("lists - Displays all to-do lists.");
    Console.WriteLine("create [name] - Creates a new to-do list.");
    Console.WriteLine("delete [name] - Deletes an existing to-do list.");
    Console.WriteLine("open [name] - Opens an existing to-do list.");
    Console.WriteLine("tasks - Displays all tasks.");
    Console.WriteLine("add [task] - Adds a new task to the list.");
    Console.WriteLine("remove [task number] - Removes a task by its number.");
    Console.WriteLine("complete [task number] - Marks a task as completed.");
    Console.WriteLine("uncomplete [task number] - Marks a task as incomplete.");
    Console.WriteLine("exit - Exits the application.");
}

void CreateList(string name)
{
    string filePath = Path.Combine(listsPath, $"{name}.txt"); 

    if (File.Exists(filePath))
    {
        Console.WriteLine("A list with that name already exists.");
        return;
    }
    File.Create(filePath).Close();
    activeList = name;
    Console.WriteLine($"List '{name}' created and opened.");
}

void DeleteList(string name)
{
    string filePath = Path.Combine(listsPath, $"{name}.txt");

    if (!File.Exists(filePath))
    {
        Console.WriteLine("No list found with that name.");
        return;
    }

    Console.WriteLine("Are you sure you want to delete this list? (yes/no)");
    if(Console.ReadLine()?.ToLower() != "yes")
    {
        Console.WriteLine("Deletion cancelled.");
        return;
    }

    File.Delete(filePath);

    if (activeList == name) activeList = null;

    Console.WriteLine($"List '{name}' deleted.");
}

void Lists()
{
    var files = Directory.GetFiles(listsPath, "*.txt");

    if (files.Length == 0)
    {
        Console.WriteLine("No to-do lists found.");
        return;
    }

    Console.WriteLine("Available To-Do Lists:");
    foreach (var file in files) Console.WriteLine($"- {Path.GetFileNameWithoutExtension(file)}");
}

void OpenList(string name)
{
    string filePath = Path.Combine(listsPath, $"{name}.txt");

    if (!File.Exists(filePath))
    {
        Console.WriteLine("No list found with that name.");
        return;
    }
    activeList = name;
    Console.WriteLine($"List '{name}' opened.");
}

void AddTask(string task)
{
    if (activeList == null)
    {
        Console.WriteLine("No active list. Please create or open a list first.");
        return;
    }

    if(string.IsNullOrWhiteSpace(task))
    {
        Console.WriteLine("Please enter a task description.");
        return;
    }

    string filePath = Path.Combine(listsPath, $"{activeList}.txt");

    File.AppendAllText(filePath, $"[ ] {task}{Environment.NewLine}");
    Console.WriteLine($"Task added to '{activeList}': {task}");
}

void ListTasks()
{
    if (activeList == null)
    {
        Console.WriteLine("No active list. Please create or open a list first.");
        return;
    }

    string filePath = Path.Combine(listsPath, $"{activeList}.txt");
    List<string> tasks = File.ReadAllLines(filePath).ToList();

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks in the list.");
        return;
    }

    Console.WriteLine($"Tasks in '{activeList}':");

    for (int i = 0; i < tasks.Count; i++) Console.WriteLine($"{i + 1}: {tasks[i]}");
}

void CompleteTask(int taskNumber)
{
    if (activeList == null)
    {
        Console.WriteLine("No active list. Please create or open a list first.");
        return;
    }

    string filePath = Path.Combine(listsPath, $"{activeList}.txt");
    List<string> tasks = File.ReadAllLines(filePath).ToList();

    if (taskNumber < 1 || taskNumber > tasks.Count)
    {
        Console.WriteLine("Invalid task number.");
        return;
    }

    tasks[taskNumber - 1] = tasks[taskNumber - 1].Replace("[ ]", "[X]");

    File.WriteAllLines(filePath, tasks);
    Console.WriteLine($"Task {taskNumber} marked as completed in '{activeList}'.");
}

void UncompleteTask(int taskNumber)
{
    if (activeList == null)
    {
        Console.WriteLine("No active list. Please create or open a list first.");
        return;
    }

    string filePath = Path.Combine(listsPath, $"{activeList}.txt");
    List<string> tasks = File.ReadAllLines(filePath).ToList();

    if (taskNumber < 1 || taskNumber > tasks.Count)
    {
        Console.WriteLine("Invalid task number.");
        return;
    }

    tasks[taskNumber - 1] = tasks[taskNumber - 1].Replace("[X]", "[ ]");

    File.WriteAllLines(filePath, tasks);
    Console.WriteLine($"Task {taskNumber} marked as incomplete in '{activeList}'.");
}
void RemoveTask(int taskNumber)
{
    if (activeList == null)
    {
        Console.WriteLine("No active list. Please create or open a list first.");
        return;
    }

    string filePath = Path.Combine(listsPath, $"{activeList}.txt");
    List<string> tasks = File.ReadAllLines(filePath).ToList();

    if (tasks.Count == 0) {
        Console.WriteLine("No tasks to remove.");
        return;
    }

    if (taskNumber < 1 || taskNumber > tasks.Count)
    {
        Console.WriteLine("Invalid task number.");
        return;
    }

    var updatedTasks = new List<string>(tasks);
    updatedTasks.RemoveAt(taskNumber - 1);
    File.WriteAllLines(filePath, updatedTasks);
    Console.WriteLine($"Task {taskNumber} removed from '{activeList}'.");
}