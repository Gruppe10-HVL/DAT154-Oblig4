using System.Reflection;
using DAT154Oblig4.Application.Common.Behaviours;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DAT154Oblig4.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddSingleton(GetConfiguredMappingConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }

    //Mapster config
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Default.AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);
        IList<IRegister> registers = config.Scan(Assembly.GetExecutingAssembly());

        config.Apply(registers);

        return config;
    }
}
