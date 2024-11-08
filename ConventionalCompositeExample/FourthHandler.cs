namespace ConventionalCompositeExample;

public class FourthHandler : AbstractTypedHandler
{
    public override void Handle(HandlerMessage message)
    {
        Console.WriteLine($"Fourth handler: {message.HandlerType}");
    }
}