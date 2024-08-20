using KEngine;
using KEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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

        protected override void Jump() {
            base.Jump();
            ac.SetAnimation("Jump", true);
            KGame.GetContent<SoundEffect>("Sound/jumpFather").Play(0.2f, (float)new Random().NextDouble()*0.4f-0.5f, 0);
        }
        void Switch() {
            ac.SetAnimation("Childless");
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
