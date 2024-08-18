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
            if (!justSpawned && Input.InteractPressed()) {
                Collider.CheckCollision(collider, out var hitInfoList);
                foreach (var hitInfo in hitInfoList)
                {
                    if (hitInfo.colliderB.GameObject.TryGetComponent<ShrimpController>(out var _)) {
                        Switch();
                    }
                }
            }
            justSpawned = false;
            base.Update(deltaTime);
        }

        void Switch() {
            GameObject.active = false;
            Transform.Position = Vector2.Zero;
            velocity = Vector2.Zero;
            father.controlling = true;
        }
    }
}
