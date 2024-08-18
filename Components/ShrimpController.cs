using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class ShrimpController : Component {
        AnimationController ac;
        protected Collider collider;
        TimeSpan timeSinceGrounded = TimeSpan.Zero;
        protected Vector2 velocity = Vector2.Zero;

        float acceleration = 0.1f;
        float maxSpeed = 0.05f;
        float jumpForce = 0.1f;
        float gravity = 0.25f;
        float horizontalVelocityInfluenceOnJump = 1f;
        float coyoteTimeSeconds = 0.2f;

        public bool controlling = true;
        public override void Initialize() {
            base.Initialize();
            ac = GameObject.GetComponent<AnimationController>();
            collider = GameObject.GetComponent<Collider>();
            collider.OnCollision += OnCollision;
        }

        private void OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (hitInfo.direction == GameConstants.Vector2.Down) {
                velocity = new Vector2(velocity.X, MathF.Max(0,velocity.Y));
                timeSinceGrounded = TimeSpan.Zero;
            }
        }



        public override void Update(float deltaTime) {
            base.Update(deltaTime);

            bool movementPressed = false;
            if (controlling) {
                if (timeSinceGrounded < TimeSpan.FromSeconds(coyoteTimeSeconds) && Input.JumpPressed()) {
                    velocity.Y = jumpForce+MathF.Abs(velocity.X)*horizontalVelocityInfluenceOnJump;
                }

                if (Input.LeftPressed()) {
                    movementPressed = true;
                    velocity.X -= acceleration*deltaTime;
                }

                if (Input.RightPressed()) {
                    movementPressed = true;
                    velocity.X += acceleration * deltaTime;
                }

            }


            if (movementPressed) {
                velocity.X = MathF.Max(MathF.Min(velocity.X, maxSpeed), -maxSpeed);
            } else {
                velocity.X += velocity.X > 0 ? -MathF.Min(velocity.X, acceleration*deltaTime) : -MathF.Max(velocity.X, -acceleration*deltaTime);
            }


            velocity.Y -= gravity*deltaTime;

            Transform.Position += velocity;

            timeSinceGrounded += TimeSpan.FromSeconds(deltaTime);
        }
    }
}
