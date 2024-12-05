using System.Collections;
using UndeadEscape.Scene.Objects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

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
    protected Map _mg;
    protected Map _fg;
    protected Map _collisions;
    protected Hand _hand;




    public PlayerCharacter playerCharacter => _playerCharacter;
    public Tile tile => _tile;
    public Skeleton skeleton => _skeleton;
    public Zombie zombie => _zombie;
    public Lich lich => _lich;
    public Hand hand => _hand;

    public Map mg => _mg;
    public Map fg => _fg;
    public Map collisons => _collisions;


    protected Level(Game game) : base(game)
    {
        _playerCharacter = new PlayerCharacter();
        _tile = new Tile();
        _skeleton = new Skeleton();
        _zombie = new Zombie();
        _lich = new Lich();
        _backgroundTile = new BackgroundTile();
        _hand = new Hand();

        _mg = new Map();
        _fg = new Map();
        _collisions = new Map();


        _scene = new ArrayList
        {
            _backgroundTile,
            
            _tile,
            _skeleton,
            _zombie,
            _lich,
            _mg,
            _fg,
            _collisions,
            _playerCharacter,
            _hand,
        };
    }



    public ArrayList Scene
    {
        get => _scene;
        set => _scene = value;
    }

   

}