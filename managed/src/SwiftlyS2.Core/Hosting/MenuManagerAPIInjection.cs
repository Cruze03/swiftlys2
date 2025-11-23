using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Menus;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuManagerAPIInjection
{
    public static IServiceCollection AddMenuManagerAPI( this IServiceCollection self )
    {
        return self.AddSingleton<MenuManagerAPI>();
    }
}