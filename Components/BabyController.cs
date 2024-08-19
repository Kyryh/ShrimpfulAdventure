using KEngine.Components;
using KEngine.Components.Colliders;
using Microsoft.Xna.Framework;
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
            Camera.MainCamera.Transform.Position = Transform.GlobalPosition;
        }

        void Switch() {
            GameObject.active = false;
            Transform.Position = Vector2.Zero;
            velocity = Vector2.Zero;
            father.controlling = true;
            father.UpdateCamera();
        }

        internal override void UpdateCamera() {
            base.UpdateCamera();
            Camera.MainCamera.Size = 25f;
        }
    }
}
