using Microsoft.Xna.Framework;
using System;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class Skeleton : IPosition, IAnimation
{
    private Vector2 _position;
    private int _animation;

    public ref Vector2 Position => ref _position;
    public ref int Animation => ref _animation;

}