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
        protected PlayerCharacter _playerCharacter;
        protected ArrayList _scene;


        public HumanPlayer(PlayerCharacter playerCharacter, ArrayList scene, Game game): base(game)
        {
            _playerCharacter = playerCharacter;
            _scene = scene;
            v = _playerCharacter.Velocity;
            isJumping = false;
        }

        private bool attacking = false;

        private float groundSpeed = 300f; // Normal ground movement speed
        private float airControlFactor = 0.3f; // Reduced control while in the air

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Reset to idle animation if not attacking
            if (!attacking)
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

            // Handle jumping
            HandleJumping(keyboard);

            // Apply gravity and handle falling
            HandleFalling(gameTime);

            // Handle attacking
            HandleAttacking(keyboard, gameTime);
        }

        private void HandleHorizontalMovement(float speed, GameTime gameTime)
        {
            bool isGrounded = _playerCharacter.OnGround;
            // Apply speed based on whether the player is grounded or not
            v.X = isGrounded ? speed : speed * airControlFactor;
            _playerCharacter.Velocity = new Vector2(v.X, _playerCharacter.Velocity.Y);
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

        private void HandleFalling(GameTime gameTime)
        {
            if (!_playerCharacter.OnGround)
            {
                // Apply gravity and fall
                _playerCharacter.Velocity += new Vector2(0, gravityForce * (float)gameTime.ElapsedGameTime.TotalSeconds);
                _playerCharacter.Position += _playerCharacter.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                _playerCharacter.Animation = 3; // Fall animation
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
                    _playerCharacter.AttackTimer = 0; // Reset timer
                }
            }
        }





    }
}
