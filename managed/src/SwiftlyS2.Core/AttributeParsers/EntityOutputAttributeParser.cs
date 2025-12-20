using System.Reflection;
using SwiftlyS2.Shared.EntitySystem;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class EntityOutputAttributeParser
{
    public static void ParseFromObject( this IEntitySystemService self, object instance )
    {
        var type = instance.GetType();
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            var entityOutputHandlerAttribute = method.GetCustomAttribute<EntityOutputHandler>();
            if (entityOutputHandlerAttribute != null)
            {
                self.HookEntityOutput(entityOutputHandlerAttribute.DesignerName, entityOutputHandlerAttribute.OutputName, method.CreateDelegate<IEntitySystemService.EntityOutputEventHandler>(instance));
            }
        }
    }
}