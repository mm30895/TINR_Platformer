using Microsoft.Xna.Framework;

namespace UndeadEscape.Physics;

public interface IPosition
{
    ref Vector2 Position { get; }
}