namespace ConventionalCompositeExample;

public abstract class AbstractTypedHandler : IHandler
{
    public abstract void Handle(HandlerMessage message);
}