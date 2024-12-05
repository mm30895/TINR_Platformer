using System;
using UndeadEscape.Graphics;
using UndeadEscape.Scene;
using Microsoft.Xna.Framework;
using UndeadEscape.Players;
using UndeadEscape.Players.Human;
using UndeadEscape.Physics;
using System.Threading;

namespace UndeadEscape;

public class Gameplay : GameComponent {
    private Level _level;
    private HumanPlayer _player;
    private GameRenderer _renderer;
    private CollisionPhysics _collisionPhysics;

    public Gameplay(Game theGame, Type levelClass) : base (theGame) {
        _init(theGame, levelClass);
        _player = new HumanPlayer(_level.playerCharacter, _level.Scene, Game);
        _collisionPhysics = new CollisionPhysics(Game, _level);

        _player.UpdateOrder = 0;
        _collisionPhysics.UpdateOrder = 1;
        _level.UpdateOrder = 2;
        UpdateOrder = 4;
        Game.Components.Add(_collisionPhysics);
        Game.Components.Add(_player);
    }
    private void _init(Game game, Type levelClass)
    {
        var args = new object[] { game };
        _level = Activator.CreateInstance(levelClass, game) as Level;
        Game.Components.Add(_level);
        _renderer = new GameRenderer(game, _level);
        Game.Components.Add(_renderer);

    }

    public override void Update(GameTime gameTime)
    {
        //_player.Update(gameTime);
        if (_player.initialHp == 0) {
            // i need to reset the level
            ResetLevel();
        }

    }
    private void ResetLevel()
    {
        // Remove existing components
        Game.Components.Remove(_player);
        Game.Components.Remove(_collisionPhysics);
        Game.Components.Remove(_level);
        Game.Components.Remove(_renderer);

        // Reinitialize level, player, and renderer
        _init(Game, _level.GetType());
        _player = new HumanPlayer(_level.playerCharacter, _level.Scene, Game);
        _collisionPhysics = new CollisionPhysics(Game, _level);

        // Set update order for new components
        _player.UpdateOrder = 0;
        _collisionPhysics.UpdateOrder = 1;
        _level.UpdateOrder = 2;
        UpdateOrder = 4;

        // Add new components to the game
        Game.Components.Add(_collisionPhysics);
        Game.Components.Add(_player);
    }
}