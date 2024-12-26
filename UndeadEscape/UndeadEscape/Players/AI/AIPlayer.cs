using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndeadEscape.Scene.Objects;
using UndeadEscape.Physics;
using System.Diagnostics;

namespace UndeadEscape.Players.AI
{
    public class AIPlayer : GameComponent
    {
        private Vector2 velocity;
        private Skeleton enemyCharacter;
        private PlayerCharacter playerCharacter;
        private float moveSpeed = 90f; // Enemy movement speed
        private float attackRange = 45f; // Distance at which enemy will attack
        private bool attacking = false;
        private float attackDuration = 400f;
        private float attackCooldown = 3000f;
        private float attackDurationTimer = 0f; // Timer for attack animation
        private float attackCooldownTimer = 0f; // Timer for attack cooldown

        public int initialHp; // Enemy's health points
        private bool takingDamage = false;
        private float damageAnimationTimer = 0f; // Timer for damage animation
        private Vector2 knockbackForce = new Vector2(100f, -100f); // Knockback force (X, Y)

        // Idle State variables
        private bool isMovingLeft = true;
        private float idleMovementTimer = 0f;
        private float chaseRange = 500f;
        private float idleMovementInterval = 1000f; // Time interval to switch direction

        public AIPlayer(Skeleton enemy, PlayerCharacter player, Game game) : base(game)
        {
            enemyCharacter = enemy;
            playerCharacter = player;
            velocity = enemyCharacter.Velocity;
            initialHp = 100;
        }

        public override void Update(GameTime gameTime)
        {
            // Handle any damage animation logic
            HandleDamageAnimation(gameTime);

            // Only process attack or movement when cooldown is finished
            if (attackCooldownTimer <= 0)
            {
                if (attacking)
                {
                    HandleAttack(gameTime); // Handle attack duration
                }
                else
                {
                    // Check if in range to attack
                    float distanceToPlayer = Vector2.Distance(enemyCharacter.Position, playerCharacter.Position);

                    if (distanceToPlayer <= attackRange)
                    {
                        StartAttack(); // Begin attack if in range
                    }
                    else if (enemyCharacter.IsActive)
                    {
                        if (distanceToPlayer > chaseRange)
                        {
                            IdleMovement(gameTime); // Perform idle movement when player is not close
                        }
                        else { 
                            MoveTowardPlayer(gameTime); // Chase the player if out of range
                        }

                    }
                }
            }
            else
            {
                // Only reduce cooldown if it's greater than 0
                attackCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Handle gravity (keep this at the end)
            HandleGravity(gameTime);
        }

        private void IdleMovement(GameTime gameTime)
        {
            // If the AI is in idle state, move left and right
            idleMovementTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (idleMovementTimer <= 0)
            {
                isMovingLeft = !isMovingLeft; // Switch direction
                idleMovementTimer = idleMovementInterval; // Reset the timer for the next direction change
            }

            if (isMovingLeft)
            {
                velocity.X = -moveSpeed; // Move left
                enemyCharacter.RotateAnimation = 1; // Face left
            }
            else
            {
                velocity.X = moveSpeed; // Move right
                enemyCharacter.RotateAnimation = 0; // Face right
            }

            // Apply horizontal movement
            enemyCharacter.Velocity = new Vector2(velocity.X, enemyCharacter.Velocity.Y);
            MovingPhysics.SimulateMovement(enemyCharacter, gameTime.ElapsedGameTime);

            enemyCharacter.Animation = 1; // Run animation (or idle animation if you prefer)
        }

        private void MoveTowardPlayer(GameTime gameTime)
        {
            if (playerCharacter.Position.X < enemyCharacter.Position.X)
            {
                velocity.X = -moveSpeed; // Move left
                enemyCharacter.RotateAnimation = 1; // Face left
            }
            else
            {
                velocity.X = moveSpeed; // Move right
                enemyCharacter.RotateAnimation = 0; // Face right
            }

            // Apply horizontal movement
            enemyCharacter.Velocity = new Vector2(velocity.X, enemyCharacter.Velocity.Y);
            MovingPhysics.SimulateMovement(enemyCharacter, gameTime.ElapsedGameTime);

            enemyCharacter.Animation = 1; // Run animation
        }

        private void StartAttack()
        {
            if (attackCooldownTimer <= 0) // Ensure cooldown is complete before attacking
            {
                attacking = true;
                attackDurationTimer = attackDuration; // Set the attack animation duration
                enemyCharacter.Attacking = true;
                enemyCharacter.Animation = 2; // Attack animation
            }
        }

        private void HandleAttack(GameTime gameTime)
        {
            if (attacking)
            {
                // Decrease the attack duration timer
                attackDurationTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (attackDurationTimer <= 0)
                {
                    // End attack
                    attacking = false;
                    enemyCharacter.Attacking = false;
                    enemyCharacter.Animation = 0; // Reset to idle
                    attackCooldownTimer = attackCooldown; // Start cooldown timer after attack completes
                }
            }
        }

        private void HandleGravity(GameTime gameTime)
        {
            if (!enemyCharacter.OnGround)
            {
                // Apply gravity
                enemyCharacter.Velocity += new Vector2(0, 550f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                enemyCharacter.Position += enemyCharacter.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                takingDamage = false;
            }

            if (enemyCharacter.OnGround)
            {
                enemyCharacter.IsActive = true;
                velocity.Y = 0; // Reset vertical velocity
            }
        }

        private void HandleDamageAnimation(GameTime gameTime)
        {
            if (initialHp > enemyCharacter.HP && damageAnimationTimer <= 0)
            {
                takingDamage = true;
                // Start damage animation and timer
                enemyCharacter.Animation = 5; // Damage animation
                damageAnimationTimer = 200f; // Damage animation duration in milliseconds
                initialHp = enemyCharacter.HP; // Update HP

                // Apply knockback based on facing direction
                float knockbackDirection = enemyCharacter.RotateAnimation == 1 ? 1 : -1; // 1 for right, -1 for left
                enemyCharacter.Velocity = new Vector2(knockbackForce.X * knockbackDirection * 2, knockbackForce.Y);
                MovingPhysics.SimulateMovement(enemyCharacter, gameTime.ElapsedGameTime);
                enemyCharacter.OnGround = false; // Ensure the player is airborne during knockback
            }

            // If damage animation timer is active, reduce it
            if (damageAnimationTimer > 0)
            {
                damageAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                enemyCharacter.Animation = 5;

                // Reset to idle or appropriate animation after timer expires
                if (damageAnimationTimer <= 0)
                {
                    enemyCharacter.Animation = 0; // Reset to idle animation
                    HandleGravity(gameTime);
                }
            }
        }

        private void HandleDeath()
        {
            enemyCharacter.Animation = 6; // Death animation
            enemyCharacter.IsActive = false; // Disable the enemy
            // Additional logic for removing the enemy from the game can go here
        }
    }
}
