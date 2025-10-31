using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when an entity touches another entity.
/// <note>This event is triggered for StartTouch, Touch, and EndTouch interactions.</note>
/// </summary>
public interface IOnEntityTouchHookEvent {

  /// <summary>
  /// Gets the entity that initiated the touch.
  /// </summary>
  public CBaseEntity Entity { get; }

  /// <summary>
  /// Gets the entity being touched.
  /// </summary>
  public CBaseEntity OtherEntity { get; }

  /// <summary>
  /// Gets or sets the hook result.
  /// Set to <see cref="HookResult.Stop"/> to prevent the touch interaction.
  /// </summary>
  public HookResult Result { get; set; } 
}