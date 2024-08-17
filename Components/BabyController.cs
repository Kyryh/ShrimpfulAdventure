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
        Collider collider;
        public override void Initialize() {
            father = Transform.Parent.GameObject.GetComponent<ShrimpController>();
            collider = GameObject.GetComponent<Collider>();
            base.Initialize();
        }
        public override void Update(float deltaTime) {
            if (Input.InteractPressed()) {
                Collider.CheckCollision(collider, out var hitInfoList);
                foreach (var hitInfo in hitInfoList)
                {
                    if (hitInfo.colliderB.GameObject.TryGetComponent<ShrimpController>(out var _)) {
                        Switch();
                    }
                }
            }
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
