namespace ConventionalCompositeExample;

public class ConventionalCompositeHandler(IReadOnlyList<IHandler> _handlers)
    : IHandler
{
    public void Handle(HandlerMessage message)
    {
        Console.WriteLine("Composite handlers:");
        foreach (var handler in _handlers)
        {
            Console.WriteLine(handler.GetType().ToString());
        }

        Console.WriteLine();
        Console.WriteLine("Results:");
        foreach (var handler in _handlers)
        {
            handler.Handle(message);
        }
    }
}