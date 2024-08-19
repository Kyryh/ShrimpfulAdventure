using KEngine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class FatherController : ShrimpController {
        BabyController baby;

        public override void Initialize() {
            base.Initialize();
            baby = Transform.children[0].GameObject.GetComponent<BabyController>();
        }
        public override void Update(float deltaTime) {
            base.Update(deltaTime);
            if (controlling && !interacted && Input.InteractPressed()) {
                Switch();
            }
        }

        void Switch() {
            controlling = false;
            baby.GameObject.active = true;
            baby.justSpawned = true;
            baby.velocity = velocity;
            baby.UpdateCamera();
        }

        internal override void UpdateCamera() {
            base.UpdateCamera();
            Camera.MainCamera.Size = 42f;
            Camera.MainCamera.Transform.Position = new Vector2(20.5f, -12.5f);
        }
    }
}
