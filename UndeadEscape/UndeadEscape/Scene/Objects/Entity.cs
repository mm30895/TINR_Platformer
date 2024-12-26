using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects
{
    public class Entity : IMovable, IAnimation, IRotateAnimation
    {
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _gravity;
        private int _animation;
        private int _sprite;
        private int _rotateAnimation;
        private bool _onGround;
        private bool _attacking;
        private int _damage;

        private int _hp;

        public ref bool OnGround => ref _onGround;
        public ref Vector2 Position => ref _position;
        public ref Vector2 Velocity => ref _velocity;
        public ref Vector2 Gravity => ref _gravity;
        public ref int Animation => ref _animation;
        public ref int Sprite => ref _sprite;
        public ref int HP => ref _hp;
        public int RotateAnimation
        {
            get => _rotateAnimation;
            set => _rotateAnimation = value;
        }

        public float AttackTimer { get; set; } = 0;
        public ref bool Attacking => ref _attacking;
        public ref int Damage => ref _damage;
    }
}
