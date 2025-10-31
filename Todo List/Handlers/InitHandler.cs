public class InitHandler : ICommandHandler
{
    public void Handle(string[] args)
    {
        try
        {
            DirectoryManager.Initialize();
            Console.WriteLine("Initialized .todo directory.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during initialization: {ex.Message}");
        }
    }
}