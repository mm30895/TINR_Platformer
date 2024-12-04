using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using UndeadEscape.Scene.Objects;

namespace UndeadEscape.Physics
{
    public static class CollisionHelper
    {
        public static IEnumerable<Rectangle> GetCollidableTiles(Map map, int tileSize)
        {
            foreach (var kvp in map.MapCsv)
            {
                if (kvp.Value == 70)
                {
                    yield return new Rectangle(
                        (int)kvp.Key.X * tileSize,
                        (int)kvp.Key.Y * tileSize,
                        tileSize,
                        45
                    );
                }
            }
        }
    }
}
