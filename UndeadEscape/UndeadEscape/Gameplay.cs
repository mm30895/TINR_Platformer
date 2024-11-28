using System;
using UndeadEscape.Graphics;
using UndeadEscape.Scene;
using Microsoft.Xna.Framework;
using UndeadEscape.Players;
using UndeadEscape.Players.Human;

namespace UndeadEscape;

public class Gameplay : GameComponent {
    private Level _level;
    private Player _player;
    private GameRenderer _renderer;

    public Gameplay(Game theGame, Type levelClass) : base (theGame) {
        _init(theGame, levelClass);
        UpdateOrder = 10;
        _player = new HumanPlayer(_level.playerCharacter, _level.Scene, Game);
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
        _player.Update(gameTime);
        
    }
}