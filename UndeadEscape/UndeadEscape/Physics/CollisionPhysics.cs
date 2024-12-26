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
                    Map map = (Map)_level.Scene[6];
                    IEnumerable<Rectangle> collidableTiles = CollisionHelper.GetCollidableTiles(map, TileSize);
                    Collision.CollisionBetween(player, collidableTiles);
                   

                    //resolving other collisions
                    foreach (object other in _level.Scene) {
                        if (other is Hand hand) {
                            Collision.CollisionBetweenHand(player, hand);
                        }
                        if (other is Skeleton skeleton) {
                            Collision.CollisionBetweenEnemy(player, skeleton);
                        }
                    }
                }
                if (item is Skeleton aiPlayer) {
                    Map map = (Map)_level.Scene[6];
                    IEnumerable<Rectangle> collidableTiles = CollisionHelper.GetCollidableTiles(map, TileSize);
                    Collision.CollisionBetween(aiPlayer, collidableTiles);
                }
            }
        }
    }
}
