using System;
using UndeadEscape.Graphics;
using UndeadEscape.Scene;
using Microsoft.Xna.Framework;
using UndeadEscape.Players;
using UndeadEscape.Players.Human;
using UndeadEscape.Physics;
using System.Threading;
using UndeadEscape.Players.AI;

namespace UndeadEscape;

public class Gameplay : GameComponent {
    private Level _level;
    private HumanPlayer _player;
    private AIPlayer _aiPlayer;
    private GameRenderer _renderer;
    private CollisionPhysics _collisionPhysics;

    public Gameplay(Game theGame, Type levelClass) : base (theGame) {
        _init(theGame, levelClass);
        _player = new HumanPlayer(_level.playerCharacter, _level.Scene, Game);
        _aiPlayer = new AIPlayer(_level.skeleton,_level.playerCharacter, Game);
        _collisionPhysics = new CollisionPhysics(Game, _level);

        _player.UpdateOrder = 0;
        _aiPlayer.UpdateOrder = 1;
        _collisionPhysics.UpdateOrder = 2;
        _level.UpdateOrder = 3;
        UpdateOrder = 4;
        Game.Components.Add(_collisionPhysics);
        Game.Components.Add(_player);
        Game.Components.Add(_aiPlayer);
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
        if (_player.initialHp < 0) {
            // i need to reset the level
            ResetLevel();
        }
        if (_aiPlayer.initialHp < 0) {
            _level.Scene.Remove(_level.skeleton);
            Game.Components.Remove(_aiPlayer);
        }

    }
    private void ResetLevel()
    {
        // Remove existing components
        Game.Components.Remove(_player);
        Game.Components.Remove(_aiPlayer);
        Game.Components.Remove(_collisionPhysics);
        Game.Components.Remove(_level);
        Game.Components.Remove(_renderer);

        // Reinitialize level, player, and renderer
        _init(Game, _level.GetType());
        _player = new HumanPlayer(_level.playerCharacter, _level.Scene, Game);
        _aiPlayer = new AIPlayer(_level.skeleton, _level.playerCharacter, Game);
        _collisionPhysics = new CollisionPhysics(Game, _level);

        // Set update order for new components
        _player.UpdateOrder = 0;
        _aiPlayer.UpdateOrder = 1;
        _collisionPhysics.UpdateOrder = 2;
        _level.UpdateOrder = 3;
        UpdateOrder = 4;

        // Add new components to the game
        Game.Components.Add(_collisionPhysics);
        Game.Components.Add(_player);
        Game.Components.Add(_aiPlayer);
    }
}