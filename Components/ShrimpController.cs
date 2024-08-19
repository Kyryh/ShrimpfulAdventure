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

        public float GroundAcceleration { get; init; } = 0.1f;
        public float AirAcceleration { get; init; } = 0.1f;
        public float GroundDeceleration { get; init; } = 0.5f;
        public float AirDeceleration { get; init; } = 0.05f;
        public float MaxSpeed { get; init; } = 0.05f;
        public float JumpForce { get; init; } = 0.1f;
        public float Gravity { get; init; } = 0.25f;
        public float CoyoteTimeSeconds { get; init; } = 0.2f;
        public float PushingForce { get; init; } = 0.02f;

        public bool controlling = true;
        public override void Initialize() {
            base.Initialize();
            ac = GameObject.GetComponent<AnimationController>();
            collider = GameObject.GetComponent<Collider>();
            collider.OnCollision += OnCollision;
        }

        private void OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (other.IsTrigger) return;
            if (hitInfo.direction == GameConstants.Vector2.Down) {
                velocity.Y = MathF.Max(0,velocity.Y);
                timeSinceGrounded = TimeSpan.Zero;
            } else if (hitInfo.direction == GameConstants.Vector2.Up) {
                velocity.Y = MathF.Min(0, velocity.Y);
            } else if (other.IsStatic) {
                if (hitInfo.direction == GameConstants.Vector2.Left) {
                    velocity.X = MathF.Max(0, velocity.X);
                } else if (hitInfo.direction == GameConstants.Vector2.Right) {
                    velocity.X = MathF.Min(0, velocity.X);
                }
            } else {
                if (hitInfo.direction == GameConstants.Vector2.Left) {
                    velocity.X = MathF.Max(-PushingForce, velocity.X);
                } else if (hitInfo.direction == GameConstants.Vector2.Right) {
                    velocity.X = MathF.Min(PushingForce, velocity.X);
                }
            }
        }

        float GetMovement(int direction, float deltaTime) {

            int sign = MathF.Sign(velocity.X);
            if (sign == 0) {
                if (direction == 0)
                    return 0f;
                sign = direction;
            }

            if (sign == direction) {
                float acceleration = sign * (timeSinceGrounded == TimeSpan.Zero ? GroundAcceleration : AirAcceleration);
                return acceleration * deltaTime;
            } else {
                float acceleration = timeSinceGrounded == TimeSpan.Zero ? GroundDeceleration : AirDeceleration;
                return -sign * MathF.Min(acceleration * deltaTime, MathF.Abs(velocity.X));
            }
        }

        public override void Update(float deltaTime) {
            base.Update(deltaTime);

            int direction = 0;
            if (controlling) {
                if (timeSinceGrounded < TimeSpan.FromSeconds(CoyoteTimeSeconds) && Input.JumpPressed()) {
                    timeSinceGrounded += TimeSpan.FromSeconds(0.2f);
                    velocity.Y = JumpForce;
                }

                if (Input.LeftPressed())
                    direction--;
                if (Input.RightPressed())
                    direction++;


            }

            velocity.X += GetMovement(direction, deltaTime);
            velocity.X = MathF.Max(MathF.Min(velocity.X, MaxSpeed), -MaxSpeed);



            velocity.Y -= Gravity*deltaTime;

            Transform.Position += velocity;

            timeSinceGrounded += TimeSpan.FromSeconds(deltaTime);
        }
    }
}
