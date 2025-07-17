using System;

namespace Journal.Journeys;

public static class Extensions
{
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<Get.Interface, Get.Implementations.Version1.Implementation>(); 
    }
}
