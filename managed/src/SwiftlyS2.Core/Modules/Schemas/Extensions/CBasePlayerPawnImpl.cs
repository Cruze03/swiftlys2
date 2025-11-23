using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerPawnImpl : CBasePlayerPawn
{
    public void CommitSuicide( bool explode, bool force )
    {
        GameFunctions.PawnCommitSuicide(Address, explode, force);
    }

    public Vector? EyePosition {
        get {
            if (!IsValid) return null;
            if (AbsOrigin == null) return null;

            var absOrigin = AbsOrigin.Value;
            var viewmodelOffset = ViewOffset;
            if (viewmodelOffset == null) return null;

            absOrigin.Z += viewmodelOffset.Z.Value;
            return absOrigin;
        }
    }

    public float GroundDistance {
        get {
            if (!IsValid) return -1f;
            if (AbsOrigin == null) return -1f;

            var start = AbsOrigin.Value;

            var angle = new QAngle(90, 0, 0);
            var rads = angle.ToRadianEuler();
            var end = start + new Vector(
                rads.X * 8192f,
                rads.Y * 8192f,
                rads.Z * 8192f
            );

            var ray = new Ray_t();
            ray.Init(start, end);

            var filter = new CTraceFilter();
            filter.QueryShapeAttributes.ObjectSetMask = RnQueryObjectSet.All;
            filter.QueryShapeAttributes.InteractsWith = MaskTrace.Sky;

            var trace = new CGameTrace();

            unsafe
            {
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, &trace);
            }

            return trace.Distance;
        }
    }
}