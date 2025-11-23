using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuManagerAPIServiceInjection
{
    public static IServiceCollection AddMenuManagerAPIService( this IServiceCollection self )
    {
        return self.AddSingleton<MenuManagerAPIService>();
    }

    public static void UseMenuManagerAPIService( this IServiceProvider self )
    {
        self.GetRequiredService<MenuManagerAPIService>();
    }
}