public class InitHandler : ICommandHandler
{

    public void Handle(string[] args)
    {
        try
        {
            if(DirectoryManager.IsInitialized())
            {
                throw new AlreadyInitializedException("The .todo directory is already initialized.");
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