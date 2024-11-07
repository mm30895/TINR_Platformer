using Microsoft.Xna.Framework;

namespace UndeadEscape.Scene.Levels;

public class Level1 : Level
{
    public Level1(Game game)
        : base(game)
    {
        _playerCharacter.Position = new Vector2(200, 200);
        _tile.Position = new Vector2(200, 264);
        _skeleton.Position = new Vector2(300, 200);
        _zombie.Position = new Vector2(400, 200);
        _lich.Position = new Vector2(500, 168);
        _backgroundTile.Position = new Vector2(200, 200);
    }

}
