public class InitHandler : ICommandHandler
{

    public void Handle(string[] args)
    {
        try
        {
            if(TodoProject.IsInitialized())
            {
                throw new AlreadyInitializedException("The .todo directory is already initialized.");
            }

            TodoProject.Initialize();
            Console.WriteLine("Initialized .todo directory.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during initialization: {ex.Message}");
        }
    }
}