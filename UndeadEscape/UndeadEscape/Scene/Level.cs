using System.Collections;
using UndeadEscape.Scene.Objects;
using Microsoft.Xna.Framework;

namespace UndeadEscape.Scene;

public class Level : GameComponent
{
    protected ArrayList _scene;
    protected BackgroundTile _backgroundTile;
    protected PlayerCharacter _playerCharacter;
    protected Tile _tile;
    protected Skeleton _skeleton;
    protected Zombie _zombie;
    protected Lich _lich;
    

    public PlayerCharacter playerCharacter => _playerCharacter;
    public Tile tile => _tile;
    public Skeleton skeleton => _skeleton;
    public Zombie zombie => _zombie;
    public Lich lich => _lich;

    protected Level(Game game) : base(game)
    {
        _playerCharacter = new PlayerCharacter();
        _tile = new Tile();
        _skeleton = new Skeleton();
        _zombie = new Zombie();
        _lich = new Lich();
        _backgroundTile = new BackgroundTile();

        _scene = new ArrayList
        {
            _backgroundTile,
            _playerCharacter,
            _tile,
            _skeleton,
            _zombie,
            _lich,
        };
    }



    public ArrayList Scene
    {
        get => _scene;
        set => _scene = value;
    }

}