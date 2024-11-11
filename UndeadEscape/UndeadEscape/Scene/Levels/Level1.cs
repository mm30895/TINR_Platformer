using Microsoft.Xna.Framework;

namespace UndeadEscape.Scene.Levels;

public class Level1 : Level
{
    public Level1(Game game)
        : base(game)
    {
        _playerCharacter.Animation = 1; // 0 = idle // 1 = running
        _playerCharacter.Position = new Vector2(50*4, 100 * 4);
        _playerCharacter.Velocity = new Vector2(5, 60);
        _tile.Position = new Vector2(50 * 4, 164 * 4);
        _skeleton.Animation = 0;
        _skeleton.Position = new Vector2(150 * 4, 100 * 4);
        _zombie.Position = new Vector2(250 * 4, 100 * 4);
        _lich.Position = new Vector2(350 * 4, 100 * 4);
        _backgroundTile.Position = new Vector2(50 * 4, 100 * 4);
    }

}
