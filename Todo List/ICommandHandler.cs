public interface ICommandHandler
{
    string? Handle(string[] args, string? activeList);
}
