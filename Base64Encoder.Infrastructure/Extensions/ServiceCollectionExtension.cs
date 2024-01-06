using Base64Encoder.Domain.Interface;
using Base64Encoder.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Base64Encoder.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection service)
        {
            service.AddSingleton<IEncoderService, EncoderService>();

            return service;
        }
    }
}
