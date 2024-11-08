namespace ConventionalCompositeExample;

public class GenericHandler : IHandler
{
    public void Handle(HandlerMessage message)
    {
        Console.WriteLine($"Saving {message.HandlerType}");
    }
}