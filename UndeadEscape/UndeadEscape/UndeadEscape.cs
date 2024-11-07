using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndeadEscape.Scene.Levels;

namespace UndeadEscape
{
    public class UndeadEscape : Game
    {

        protected GraphicsDeviceManager _graphics;
        protected Gameplay _currentGameplay;
        protected List<Type> _levelClasses;

        public UndeadEscape()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            _levelClasses = new List<Type> { typeof(Level1) };
            this.LoadMultiplayerLevel(_levelClasses[0]);
            base.Initialize();
        }
        public void LoadMultiplayerLevel(Type levelClass)
        {
            if (_currentGameplay is not null)
            {
                this.Components.Remove(_currentGameplay);
            }

            _currentGameplay = new Gameplay(this, levelClass);
            this.Components.Add(_currentGameplay);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


    }
}
