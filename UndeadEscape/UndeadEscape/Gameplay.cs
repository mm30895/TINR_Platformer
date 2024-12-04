using System;
using UndeadEscape.Graphics;
using UndeadEscape.Scene;
using Microsoft.Xna.Framework;
using UndeadEscape.Players;
using UndeadEscape.Players.Human;
using UndeadEscape.Physics;

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

    }
}