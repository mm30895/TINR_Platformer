using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndeadEscape.Physics;

namespace UndeadEscape.Scene.Objects;

public class Map
{
    private Dictionary<Vector2, int> _map;
    private bool _drawable;

    public ref Dictionary<Vector2, int> MapCsv => ref _map;
    public ref bool Drawable => ref _drawable;

}