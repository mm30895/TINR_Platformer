using System.Collections;
using UndeadEscape.Scene.Objects;
using Microsoft.Xna.Framework;

namespace UndeadEscape.Players;

public abstract class Player
{

    protected PlayerCharacter _playerCharacter;
    protected ArrayList _scene;

    protected Player(PlayerCharacter playerCharacter, ArrayList scene)
    {
        _playerCharacter = playerCharacter;
        _scene = scene;
    }
    public virtual void Update(GameTime gameTime)
    {
    }
}