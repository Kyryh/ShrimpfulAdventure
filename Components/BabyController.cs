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
        ShrimpController father;
        public bool justSpawned;
        public override void Initialize() {
            father = Transform.Parent.GameObject.GetComponent<ShrimpController>();

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
            Camera.MainCamera.Transform.Position = GetCameraPosition();
        }

        protected override void Jump() {
            base.Jump();
            KGame.GetContent<SoundEffect>("Sound/jumpBaby").Play(0.2f, (float)new Random().NextDouble() * 0.4f - 0.5f, 0);
        }

        void Switch() {
            GameObject.active = false;
            Transform.Position = Vector2.Zero;
            velocity = Vector2.Zero;
            father.controlling = true;
            father.UpdateCamera();
            father.ac.SetAnimation("Idle");
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
