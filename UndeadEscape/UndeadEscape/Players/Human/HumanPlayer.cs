using System;
using System.Collections;
using UndeadEscape.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndeadEscape.Physics;


namespace UndeadEscape.Players.Human;

public class HumanPlayer : Player {
    protected Vector2 v;
    protected bool isJumping;
    protected bool isFalling;
    protected float groundLevel;
    public HumanPlayer(PlayerCharacter playerCharacter, ArrayList scene, Game game) : base (playerCharacter, scene) { 
        _playerCharacter = playerCharacter;
        _scene = scene;
        v = new Vector2(_playerCharacter.Velocity.X, _playerCharacter.Velocity.Y);
        isJumping = false;
        isFalling = true;
        groundLevel = 400f;

    }
    private bool atacking = false;

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();

        //_playerCharacter.Animation = 0; // set to idle animation
        //_playerCharacter.RotateAnimation = 0; 

        if (!atacking) {
            _playerCharacter.Animation = 0;
        }


        if (keyboard.IsKeyDown(Keys.A))
        {
            //move left
            if (v.X > 0)
            {
                v.X *= -1;
            }
            _playerCharacter.Velocity = new Vector2(v.X,0);
            MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);

            _playerCharacter.Animation = 1; // set animation to run
            _playerCharacter.RotateAnimation = 1; // rotate

        }
        if (keyboard.IsKeyDown(Keys.D))
           
        {
            //move right
            if (v.X < 0)
            {
                v.X *= -1;
            }
            _playerCharacter.Velocity = new Vector2(v.X, 0);
            MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);

            _playerCharacter.Animation = 1; // set animation to run
            _playerCharacter.RotateAnimation = 0; 

        }
        if (keyboard.IsKeyDown(Keys.Space) && !isJumping && !isFalling)
        {
            isJumping = true;
            isFalling = false;
            v.Y = -800f; // jump velocity
            _playerCharacter.Gravity = Vector2.Zero; // disable gravity
        }

        if (isJumping)
        {
            _playerCharacter.Position += new Vector2(0, v.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            v.Y += 30f;
            _playerCharacter.Animation = 4;

            if (v.Y >= 0) // peak height
            {
                isJumping = false;
                isFalling = true;
                _playerCharacter.Gravity = new Vector2(0, 550f); //enable gravity
            }
        }

        if (isFalling)
        {
            MovingPhysics.SimulateGravity(_playerCharacter, gameTime.ElapsedGameTime);
            _playerCharacter.Animation = 3;
        }

        if (_playerCharacter.Position.Y >= groundLevel) // seting a fake collider
        {
            isFalling = false;
            _playerCharacter.Position = new Vector2(_playerCharacter.Position.X, groundLevel);
            v.Y = 0;
        }

        if (keyboard.IsKeyDown(Keys.E) && !atacking)
        {
            atacking = true;
            _playerCharacter.Animation = 2; // Attack animation
            _playerCharacter.AttackTimer = 400; // 400 ms for 4 frames at 60 FPS
        }

        if (atacking)
        {
            _playerCharacter.Animation = 2; // Attack animation

            // Reduce attack timer
            _playerCharacter.AttackTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_playerCharacter.AttackTimer <= 0)
            {
                atacking = false; // Attack finished
                _playerCharacter.AttackTimer = 0; // Reset timer
            }

            return; // Skip other actions while attacking
        }

    }

}


