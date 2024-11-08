using ConventionalCompositeExample;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection().AddHandlers();

var serviceProvider = services.BuildServiceProvider();

foreach (var handlerType in Enum.GetValues<HandlerType>())
{
    var handler =
        serviceProvider.GetRequiredKeyedService<IHandler>(handlerType.ToString());

    Console.WriteLine($"Executing {handlerType} handler");
    Console.WriteLine(handler.GetType());

    handler.Handle(new HandlerMessage(handlerType));

    Console.WriteLine();
    Console.WriteLine();
}
