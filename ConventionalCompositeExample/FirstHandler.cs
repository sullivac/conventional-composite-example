namespace ConventionalCompositeExample;

public class FirstHandler : AbstractTypedHandler
{
    public override void Handle(HandlerMessage message)
    {
        Console.WriteLine($"First handler: {message.HandlerType}");
    }
}