using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityTouchHookEvent : IOnEntityTouchHookEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }

  public HookResult Result { get; set; } = HookResult.Continue;
}