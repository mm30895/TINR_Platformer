using Microsoft.Xna.Framework;
using System;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class PlayerCharacter : IMovable, IAnimation, IRotateAnimation
{
    private Vector2 _position;
    private Vector2 _velocity;
    private Vector2 _gravity;
    private int _animation;
    private int _sprite;
    private int _rotateAnimation;
    private bool _onGround;

    public ref bool OnGround => ref _onGround;
    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;
    public ref Vector2 Gravity => ref _gravity;
    public ref int Animation => ref _animation;
    public ref int Sprite => ref _sprite;
    public int RotateAnimation
    {
        get => _rotateAnimation;
        set => _rotateAnimation = value;
    }

    public float AttackTimer { get; set; } = 0;


}