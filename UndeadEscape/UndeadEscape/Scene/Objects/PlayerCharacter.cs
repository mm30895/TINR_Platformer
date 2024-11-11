using Microsoft.Xna.Framework;
using System;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class PlayerCharacter : IMovable, IAnimation, IRotateAnimation
{
    private Vector2 _position;
    private Vector2 _velocity;
    private int _animation;
    private int _rotateAnimation;

    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;
    public ref int Animation => ref _animation;
    public int RotateAnimation
    {
        get => _rotateAnimation;
        set => _rotateAnimation = value;
    }

}