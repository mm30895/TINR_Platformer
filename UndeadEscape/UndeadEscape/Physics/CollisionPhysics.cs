using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndeadEscape.Physics.Collisions;
using UndeadEscape.Scene;
using UndeadEscape.Scene.Objects;

namespace UndeadEscape.Physics
{
    public class CollisionPhysics : GameComponent
    {
        protected Level _level;
        private const int TileSize = 128; // Tile dimensions

        public CollisionPhysics(Game game, Level level)
            : base(game)
        {
            _level = level;
        }

        public override void Update(GameTime gameTime)
        {
            // Simulate movement
            //foreach (var item in _level.Scene)
            //{
            //    MovingPhysics.SimulateMovement(item, gameTime.ElapsedGameTime);
            //}
            
            // Handle collisions
            foreach (object item in _level.Scene)
            {
                if (item is PlayerCharacter player)
                {
                    //player.Position = new Vector2(200, 200);
                    // Get collidable tiles from the map
                    Map map = (Map)_level.Scene[7];
                    IEnumerable<Rectangle> collidableTiles = CollisionHelper.GetCollidableTiles(map, TileSize);

                    // Resolve collisions between the player and the tiles
                    Collision.CollisionBetween(player, collidableTiles);
                }
            }
        }
    }
}
