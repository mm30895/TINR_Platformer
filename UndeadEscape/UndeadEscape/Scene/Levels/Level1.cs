using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace UndeadEscape.Scene.Levels;

public class Level1 : Level
{
    public Level1(Game game)
        : base(game)
    {
        _playerCharacter.Animation = 2; // 0 = idle // 1 = running // 2 = attack
        _playerCharacter.Sprite = 0; // dont use sprites
        _playerCharacter.Position = new Vector2(50, 100 );
        _playerCharacter.Velocity = new Vector2(250, 0);
        _playerCharacter.Gravity = new Vector2(0, 400);
        //_tile.Position = new Vector2(50 * 4, 164 * 4);
        _skeleton.Animation = 0;
        _skeleton.Sprite = 0;
        _skeleton.Position = new Vector2(150 * 4, 100 * 4);
        _skeleton.Velocity = new Vector2(250, 0);
        _zombie.Position = new Vector2(250 * 4, 100 * 4);
        _lich.Position = new Vector2(350 * 4, 100 * 4);
        //_backgroundTile.Position = new Vector2(50 * 4, 100 * 4);

        _fg.MapCsv = LoadMap("../../../Scene/LevelData/testLevel._foreground.csv");
        _mg.MapCsv = LoadMap("../../../Scene/LevelData/testLevel._midground.csv");
        _collisions.MapCsv = LoadMap("../../../Scene/LevelData/testLevel._collisions.csv");
        _fg.Drawable = true;
        _mg.Drawable = true;
        _collisions.Drawable = false;


    }


    private Dictionary<Vector2, int> LoadMap(string filepath) { 
        Dictionary<Vector2, int> result = new();
        StreamReader reader = new StreamReader(filepath);
        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++) {
                if (int.TryParse(items[x], out int value)) {
                    if (value > -1) {
                        result[new Vector2(x, y)] = value;
                    }
                }
            }
            y++;
        }
        return result;
    }

}
