using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBasePlayerPawn
{
    /// <summary>
    /// Performs a suicide on the pawn, optionally causing an explosion and allowing forced execution.
    /// </summary>
    public void CommitSuicide( bool explode, bool force );

    public Vector? EyePosition { get; }
    public float GroundDistance { get; }
}