using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UndeadEscape.Scene.Objects;

namespace UndeadEscape.Physics.Collisions
{
    public static class Collision
    {
        public static void CollisionBetween(Entity player, IEnumerable<Rectangle> collidableTiles)
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

        public static void CollisionBetweenEnemy(Entity player, Entity skeleton)
        {

            Rectangle playerBounds = new Rectangle(
                (int)player.Position.X - 64,
                (int)player.Position.Y - 64,
                128,
                128
            );

            Rectangle skeletonBounds = new Rectangle(
                (int)skeleton.Position.X,
                (int)skeleton.Position.Y,
                8,
                32
            );

            if (playerBounds.Intersects(skeletonBounds))
            {
                int overlapX = Math.Min(playerBounds.Right, skeletonBounds.Right) - Math.Max(playerBounds.Left, skeletonBounds.Left);
                int overlapY = Math.Min(playerBounds.Bottom, skeletonBounds.Bottom) - Math.Max(playerBounds.Top, skeletonBounds.Top);

                if (Math.Abs(overlapX) < Math.Abs(overlapY))
                {
                    //if (playerBounds.Center.X < skeletonBounds.Center.X)
                    //{
                    //    player.Position.X -= overlapX - 3;
                    //    //skeleton.Position.X -= overlapX + 3;

                    //}
                    //else
                    //{
                    //    player.Position.X += overlapX - 3;
                    //    //skeleton.Position.X += overlapX - 3;
                    //}

                    //player.Velocity.X = 0;
                    //skeleton.Velocity.X = 0;
                    if (player.Attacking) {
                        skeleton.HP -= player.Damage;
                    }
                    if (skeleton.Attacking)
                    {
                        player.HP -= skeleton.Damage;
                    }
                }
                else
                {
                    //if (playerBounds.Center.Y < skeletonBounds.Center.Y)
                    //{
                    //    player.Position.Y -= overlapY + 3;
                    //}
                    //else
                    //{
                    //    player.Position.Y += overlapY - 3;
                    //}

                    //player.Velocity.Y = 0;
                    //skeleton.Velocity.Y = 0;
                }
            }
        }
    }
}
