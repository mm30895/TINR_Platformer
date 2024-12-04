using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UndeadEscape.Scene.Objects;

namespace UndeadEscape.Physics.Collisions
{
    public static class Collision
    {
        public static void CollisionBetween(PlayerCharacter player, IEnumerable<Rectangle> collidableTiles)
        {
            Rectangle playerBounds = new Rectangle(
                (int)player.Position.X - 64,
                (int)player.Position.Y - 64,
                128,
                128
            );

            bool isGrounded = false;

            foreach (var tileBounds in collidableTiles)
            {
                if (playerBounds.Intersects(tileBounds))
                {
                    int overlapX = Math.Min(playerBounds.Right, tileBounds.Right) - Math.Max(playerBounds.Left, tileBounds.Left);
                    int overlapY = Math.Min(playerBounds.Bottom, tileBounds.Bottom) - Math.Max(playerBounds.Top, tileBounds.Top);

                    if (Math.Abs(overlapX) < Math.Abs(overlapY))
                    {
                        if (playerBounds.Center.X < tileBounds.Center.X)
                        {
                            player.Position.X -= overlapX;
                        }
                        else
                        {
                            player.Position.X += overlapX;
                        }

                        player.Velocity.X = 0;
                    }
                    else
                    {
                        if (playerBounds.Center.Y < tileBounds.Center.Y)
                        {
                            player.Position.Y -= overlapY -1; // Player lands on the ground
                            isGrounded = true;
                        }
                        else
                        {
                            player.Position.Y += overlapY; // Player collides from below
                        }

                        player.Velocity.Y = 0;
                    }
                }
            }

            player.OnGround = isGrounded;
        }


    }
}
