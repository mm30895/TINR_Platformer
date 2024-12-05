using Microsoft.Xna.Framework;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class Hand : IPosition, IAnimation
{
    private Vector2 _position;
    private int _animation;
    private int _sprite;
    private int _damage;

    public ref Vector2 Position => ref _position;
    public ref int Animation => ref _animation;
    public ref int Sprite => ref _sprite;
    public ref int Damage => ref _damage;

}