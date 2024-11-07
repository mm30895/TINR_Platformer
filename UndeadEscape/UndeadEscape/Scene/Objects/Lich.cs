using Microsoft.Xna.Framework;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class Lich : IPosition
{
    private Vector2 _position;

    public ref Vector2 Position => ref _position;

}