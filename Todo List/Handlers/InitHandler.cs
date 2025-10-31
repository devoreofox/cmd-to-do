public class InitHandler : ICommandHandler
{
    public void Handle(string[] args)
    {
        try
        {
            if(DirectoryManager.IsInitialized())
            {
                Console.Error.WriteLine("Error: .todo directory already exists. Initialization aborted.");
                return;
            }
            DirectoryManager.Initialize();
            Console.WriteLine("Initialized .todo directory.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during initialization: {ex.Message}");
        }
    }
}