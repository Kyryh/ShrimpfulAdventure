using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class BabyController : ShrimpController {
        public static BabyController Instance { private set; get; }
        public bool justSpawned;
        SoundEffect jumpSound;
        public override void Initialize() {
            jumpSound = KGame.GetContent<SoundEffect>("Sound/jumpBaby");
            Instance = this;
            base.Initialize();
        }
        public override void Update(float deltaTime) {
            base.Update(deltaTime);
            if (!justSpawned && !interacted && Input.InteractPressed()) {
                Collider.CheckCollision(collider, out var hitInfoList);
                foreach (var hitInfo in hitInfoList)
                {
                    if (hitInfo.colliderB.GameObject.TryGetComponent<ShrimpController>(out var _)) {
                        Switch();
                        return;
                    }
                }
            }
            justSpawned = false;
            
            Camera.MainCamera.Transform.Position = Vector2.Lerp(Camera.MainCamera.Transform.Position, GetCameraPosition(), .1f);
        }

        protected override void Jump() {
            base.Jump();
            jumpSound.Play(0.2f, (float)ShrimpfulGame.Random.NextDouble() * 0.4f - 0.5f, 0);
        }

        void Switch() {
            GameObject.active = false;
            Transform.Position = Vector2.Zero;
            velocity = Vector2.Zero;
            FatherController.Instance.controlling = true;
            FatherController.Instance.UpdateCamera();
            FatherController.Instance.ac.SetAnimation("Idle");
        }

        Vector2 GetCameraPosition() {
            var position = Transform.GlobalPosition;
            position.X = MathF.Min(MathF.Max(position.X, 12), 29);
            position.Y = MathF.Min(MathF.Max(position.Y, -18), -7);
            return position;
        }
        internal override void UpdateCamera() {
            base.UpdateCamera();
            Camera.MainCamera.Size = 25f;
            Camera.MainCamera.Transform.Position = GetCameraPosition();
        }
    }
}
