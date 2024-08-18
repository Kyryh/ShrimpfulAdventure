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

        public float Acceleration { get; init; } = 0.1f;
        public float MaxSpeed { get; init; } = 0.05f;
        public float JumpForce { get; init; } = 0.1f;
        public float Gravity { get; init; } = 0.25f;
        public float HorizontalVelocityInfluenceOnJump { get; init; } = 1f;
        public float CoyoteTimeSeconds { get; init; } = 0.2f;

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
                if (timeSinceGrounded < TimeSpan.FromSeconds(CoyoteTimeSeconds) && Input.JumpPressed()) {
                    velocity.Y = JumpForce+MathF.Abs(velocity.X)*HorizontalVelocityInfluenceOnJump;
                }

                if (Input.LeftPressed()) {
                    movementPressed = true;
                    velocity.X -= Acceleration*deltaTime;
                }

                if (Input.RightPressed()) {
                    movementPressed = true;
                    velocity.X += Acceleration * deltaTime;
                }

            }


            if (movementPressed) {
                velocity.X = MathF.Max(MathF.Min(velocity.X, MaxSpeed), -MaxSpeed);
            } else {
                velocity.X += velocity.X > 0 ? -MathF.Min(velocity.X, Acceleration*deltaTime) : -MathF.Max(velocity.X, -Acceleration*deltaTime);
            }


            velocity.Y -= Gravity*deltaTime;

            Transform.Position += velocity;

            timeSinceGrounded += TimeSpan.FromSeconds(deltaTime);
        }
    }
}
