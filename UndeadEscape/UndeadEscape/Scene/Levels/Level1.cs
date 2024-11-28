using Microsoft.Xna.Framework;

namespace UndeadEscape.Scene.Levels;

public class Level1 : Level
{
    public Level1(Game game)
        : base(game)
    {
        _playerCharacter.Animation = 2; // 0 = idle // 1 = running // 2 = attack
        _playerCharacter.Sprite = 0; // dont use sprites
        _playerCharacter.Position = new Vector2(50*4, 100 * 4);
        _playerCharacter.Velocity = new Vector2(250, 0);
        _playerCharacter.Gravity = new Vector2(0, 200);
        _tile.Position = new Vector2(50 * 4, 164 * 4);
        _skeleton.Animation = 0;
        _skeleton.Sprite = 0;
        _skeleton.Position = new Vector2(150 * 4, 100 * 4);
        _skeleton.Velocity = new Vector2(250, 0);
        _zombie.Position = new Vector2(250 * 4, 100 * 4);
        _lich.Position = new Vector2(350 * 4, 100 * 4);
        _backgroundTile.Position = new Vector2(50 * 4, 100 * 4);
    }

}
