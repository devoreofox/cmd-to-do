public class TodoItem
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public TodoItem(string description)
    {
        Description = description;
        IsCompleted = false;
    }
    public override string ToString()
    {
        return $"{(IsCompleted ? "[X]" : "[ ]")} {Description}";
    }
}