﻿using Microsoft.Xna.Framework;
using UndeadEscape.Physics;

namespace UndeadEscape.Scene.Objects;

public class Tile : IPosition
{
    private Vector2 _position;

    public ref Vector2 Position => ref _position;

}