using Microsoft.Xna.Framework;
using System;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class Skeleton : IMovable, IAnimation
{
    private Vector2 _position;
    private int _animation;
    private int _sprite;
    private Vector2 _velocity;
    private Vector2 _gravity;
    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;
    public ref Vector2 Gravity => ref _gravity;
    public ref int Animation => ref _animation;

    public ref int Sprite => ref _sprite;

}