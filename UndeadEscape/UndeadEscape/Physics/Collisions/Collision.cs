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

        public static void CollisionBetweenHand(PlayerCharacter player, Hand hand) {

            Rectangle playerBounds = new Rectangle(
                (int)player.Position.X - 64,
                (int)player.Position.Y - 64,
                128,
                128
            );

            Rectangle handBounds = new Rectangle(
                (int)hand.Position.X,
                (int)hand.Position.Y - 32,
                8,
                8
            );

            if (playerBounds.Intersects(handBounds)) {
                int overlapX = Math.Min(playerBounds.Right, handBounds.Right) - Math.Max(playerBounds.Left, handBounds.Left);
                int overlapY = Math.Min(playerBounds.Bottom, handBounds.Bottom) - Math.Max(playerBounds.Top, handBounds.Top);

                if (Math.Abs(overlapX) < Math.Abs(overlapY))
                {
                    if (playerBounds.Center.X < handBounds.Center.X)
                    {
                        player.Position.X -= overlapX;
                        
                    }
                    else
                    {
                        player.Position.X += overlapX;
                    }

                    player.Velocity.X = 0;
                    player.HP -= hand.Damage;
                }
                else
                {
                    if (playerBounds.Center.Y < handBounds.Center.Y)
                    {
                        player.Position.Y -= overlapY;
                    }
                    else
                    {
                        player.Position.Y += overlapY;
                    }

                    player.Velocity.Y = 0;
                    player.HP -= hand.Damage;
                }
            }

        }


    }
}
