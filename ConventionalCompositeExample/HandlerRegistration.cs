using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace ConventionalCompositeExample;

public static class HandlerRegistration
{
    private static readonly IEnumerable<Type> ConcreteHandlerTypes =
        typeof(IHandler).Assembly
            .GetTypes()
            .Where(InheritsAbstractTypedHandler)
            .ToList();

    private static readonly Dictionary<string, Type> ConcreteHandlerTypeMap =
            ConcreteHandlerTypes.ToDictionary(type => type.Name, type => type);

    private static readonly IEnumerable<HandlerType> HandlerTypes =
        Enum.GetValues<HandlerType>();

    public static IServiceCollection AddHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<GenericHandler>();

        foreach (var handlerType in HandlerTypes)
        {
            if (TryGetAbstractTypeHandlerType(
                handlerType,
                out var abstractTypeHandlerType))
            {
                services.AddScoped(abstractTypeHandlerType);
            }

            services.AddKeyedScoped(
                typeof(IHandler),
                handlerType.ToString(),
                (serviceProvider, key) =>
                {
                    var handlers =
                        GetHandlers(serviceProvider, handlerType).ToList();

                    var result = new ConventionalCompositeHandler(handlers);

                    return result;
                });
        }

        return services;
    }

    private static bool InheritsAbstractTypedHandler(Type type)
    {
        return !type.IsAbstract
            && type.IsAssignableTo(typeof(AbstractTypedHandler));
    }

    private static bool TryGetAbstractTypeHandlerType(
        HandlerType handlerType,
        [MaybeNullWhen(false)] out Type abstractTypeHandlerType)
    {
        var abstractTypedHandlerTypeName = handlerType + "Handler";
        if (ConcreteHandlerTypeMap.TryGetValue(
            abstractTypedHandlerTypeName,
            out var concreteHandlerType))
        {
            abstractTypeHandlerType = concreteHandlerType;

            return true;
        }

        abstractTypeHandlerType = default;

        return false;
    }

    private static IEnumerable<IHandler> GetHandlers(
        IServiceProvider serviceProvider,
        HandlerType handlerType)
    {
        if (TryGetAbstractTypeHandlerType(
            handlerType,
            out var abstractTypeHandlerType))
        {
            var requiredService =
                serviceProvider.GetRequiredService(abstractTypeHandlerType);

            if (requiredService is IHandler handler)
            {
                yield return handler;
            }
        }

        yield return serviceProvider.GetRequiredService<GenericHandler>();
    }
}