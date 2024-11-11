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
    public HumanPlayer(PlayerCharacter playerCharacter, ArrayList scene, Game game) : base (playerCharacter, scene) { 
        _playerCharacter = playerCharacter;
        _scene = scene;
        v = new Vector2(_playerCharacter.Velocity.X, _playerCharacter.Velocity.Y);
        isJumping = false;

    }

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();
        
        _playerCharacter.Animation = 0; // set to idle animation
        //_playerCharacter.RotateAnimation = 0; 



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
            //move left
            if (v.X < 0)
            {
                v.X *= -1;
            }
            _playerCharacter.Velocity = new Vector2(v.X, 0);
            MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);

            _playerCharacter.Animation = 1; // set animation to run
            _playerCharacter.RotateAnimation = 0; 

        }
        if (keyboard.IsKeyDown(Keys.Space) && !isJumping)
        {

            isJumping = true;
            if (v.Y > 0)
            {
                v.Y *= -1;
            }
            _playerCharacter.Velocity = new Vector2(0, v.Y);
            MovingPhysics.SimulateMovement(_playerCharacter, gameTime.ElapsedGameTime);

            _playerCharacter.Animation = 1; // set animation to run
            _playerCharacter.RotateAnimation = 0;
        }
        if(!keyboard.IsKeyDown(Keys.Space)) {
            isJumping = false;
        }

    }

}


