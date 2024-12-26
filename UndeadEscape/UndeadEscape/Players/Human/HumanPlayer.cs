using System;
using System.Collections;
using UndeadEscape.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndeadEscape.Physics;

namespace UndeadEscape.Players.Human
{
    public class HumanPlayer : GameComponent
    {
        protected Vector2 v;
        protected Camera _camera;
        protected bool isJumping;
        protected float jumpVelocity = -400f;
        protected float gravityForce = 550f;
        public int initialHp;
        protected PlayerCharacter _playerCharacter;
        protected ArrayList _scene;


        public HumanPlayer(PlayerCharacter playerCharacter, ArrayList scene, Game game): base(game)
        {
            _playerCharacter = playerCharacter;
            _scene = scene;
            v = _playerCharacter.Velocity;
            isJumping = false;
            initialHp = 100;
        }

        private bool attacking = false;
        private bool takingDamage = false;

        private float groundSpeed = 300f; // Normal ground movement speed
        private float airControlFactor = 0.3f; // Reduced control while in the air


        private float damageAnimationTimer = 0f; // Timer to track damage animation
        private Vector2 knockbackForce = new Vector2(500f, -300f); // Knockback force (X, Y)

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Reset to idle animation if not attacking or damaged
            if (!attacking && damageAnimationTimer <= 0)
            {
                _playerCharacter.Animation = 0;
            }

            // Handle horizontal movement (left and right)
            if (keyboard.IsKeyDown(Keys.A))
            {
                HandleHorizontalMovement(-groundSpeed, gameTime);
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                HandleHorizontalMovement(groundSpeed, gameTime);
            }
            else
            {
                // Slow down the horizontal movement to simulate friction
                v.X = 0;
                _playerCharacter.Velocity = new Vector2(v.X, _playerCharacter.Velocity.Y);
            }

            // Handle damage animation and knockback
            HandleDamageAnimation(gameTime);

            
            if (!takingDamage) {
                // Handle jumping
                HandleJumping(keyboard);

                // Apply gravity and handle falling
                HandleFalling(gameTime, 3);

                // Handle attacking
                HandleAttacking(keyboard, gameTime);
            }
        }

        private void HandleDamageAnimation(GameTime gameTime)
        {
            // Check if the player took damage
            if (initialHp > _playerCharacter.HP && damageAnimationTimer <= 0)
            {
                takingDamage = true;
                // Start damage animation and timer
                _playerCharacter.Animation = 5; // Damage animation
                damageAnimationTimer = 200f; // Damage animation duration in milliseconds
                initialHp = _playerCharacter.HP; // Update HP

                // Apply knockback based on facing direction
                float knockbackDirection = _playerCharacter.RotateAnimation == 1 ? 1 : -1; // 1 for right, -1 for left
                _playerCharacter.Velocity = new Vector2(knockbackForce.X * knockbackDirection *2, knockbackForce.Y);
                MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);
                _playerCharacter.OnGround = false; // Ensure the player is airborne during knockback
            }

            // If damage animation timer is active, reduce it
            if (damageAnimationTimer > 0)
            {
                damageAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                _playerCharacter.Animation = 5;

                // Reset to idle or appropriate animation after timer expires
                if (damageAnimationTimer <= 0)
                {
                    _playerCharacter.Animation = 0; // Reset to idle animation
                    HandleFalling(gameTime, 5);
                }
            }
        }


        private void HandleHorizontalMovement(float speed, GameTime gameTime)
        {
            bool isGrounded = _playerCharacter.OnGround;
            // Apply speed based on whether the player is grounded or not
            v.X = isGrounded ? speed : speed * airControlFactor;
            _playerCharacter.Velocity = new Vector2(v.X, _playerCharacter.Velocity.Y);
            if (!isGrounded)
            {
                _playerCharacter.Velocity = new Vector2(v.X, _playerCharacter.Velocity.Y);
            }
            else {
                _playerCharacter.Velocity = new Vector2(v.X, 0);
            }
            MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);


            _playerCharacter.Animation = 1; // Run animation
            _playerCharacter.RotateAnimation = v.X < 0 ? 1 : 0; // Flip animation based on direction
        }

        private void HandleJumping(KeyboardState keyboard)
        {
            bool isGrounded = _playerCharacter.OnGround;

            if (keyboard.IsKeyDown(Keys.Space) && isGrounded && !isJumping)
            {
                isJumping = true;
                _playerCharacter.OnGround = false; // Player is no longer grounded
                _playerCharacter.Velocity = new Vector2(0, jumpVelocity); // Set X velocity to 0 to stop drifting
                _playerCharacter.Animation = 4; // Jump animation
            }
        }

        private void HandleFalling(GameTime gameTime, int animation)
        {
            if (!_playerCharacter.OnGround)
            {

                if (_playerCharacter.Velocity.Y > 0 && !(damageAnimationTimer > 0))
                {
                    _playerCharacter.Animation = animation;
                }
                else if (_playerCharacter.Velocity.Y < 0 && !(damageAnimationTimer > 0)) {
                    _playerCharacter.Animation = 4;
                }
                // Apply gravity and fall
                _playerCharacter.Velocity += new Vector2(0, gravityForce * (float)gameTime.ElapsedGameTime.TotalSeconds);
                _playerCharacter.Position += _playerCharacter.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //_playerCharacter.Animation = animation; // Fall animation
                takingDamage = false;
            }

            // If grounded, reset jump and falling states
            if (_playerCharacter.OnGround)
            {
                isJumping = false;
                v.Y = 0; // Reset vertical velocity when on the ground
                //_playerCharacter.Animation = 0; // Idle animation
                
            }
        }


        private void HandleAttacking(KeyboardState keyboard, GameTime gameTime)
        {
            if (keyboard.IsKeyDown(Keys.E) && !attacking)
            {
                attacking = true;
                _playerCharacter.Attacking = true;
                _playerCharacter.Animation = 2; // Attack animation
                _playerCharacter.AttackTimer = 400; // 400 ms attack duration
            }

            if (attacking)
            {
                // Reduce attack timer
                _playerCharacter.AttackTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_playerCharacter.AttackTimer <= 0)
                {
                    attacking = false; // Attack finished
                    _playerCharacter.Attacking = false;
                    _playerCharacter.AttackTimer = 3; // Reset timer
                }
            }
        }





    }
}
