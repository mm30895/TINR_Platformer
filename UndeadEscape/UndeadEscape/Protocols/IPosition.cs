using Microsoft.Xna.Framework;

namespace UndeadEscape.Protocols;

public interface IPosition
{
    ref Vector2 Position { get; }
}