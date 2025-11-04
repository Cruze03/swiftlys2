namespace SwiftlyS2.Shared.SchemaDefinitions
{
    public enum EntityTouchType : byte
    {
        StartTouch = 0,
        Touch = 1,
        EndTouch = 2
    }
}

namespace SwiftlyS2.Shared.Events
{
    using SwiftlyS2.Shared.SchemaDefinitions;

    /// <summary>
    /// Called when an entity touches another entity.
    /// <note>This event is triggered for StartTouch, Touch, and EndTouch interactions.</note>
    /// </summary>
    public interface IOnEntityTouchHookEvent
    {

        /// <summary>
        /// Gets the entity that initiated the touch.
        /// </summary>
        public CBaseEntity Entity { get; }

        /// <summary>
        /// Gets the entity being touched.
        /// </summary>
        public CBaseEntity OtherEntity { get; }

        /// <summary>
        /// Gets the type of touch interaction.
        /// </summary>
        public EntityTouchType TouchType { get; }
    }
}