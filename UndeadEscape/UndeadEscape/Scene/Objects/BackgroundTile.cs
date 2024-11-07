﻿using Microsoft.Xna.Framework;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class BackgroundTile : IPosition
{
    private Vector2 _position;

    public ref Vector2 Position => ref _position;

}