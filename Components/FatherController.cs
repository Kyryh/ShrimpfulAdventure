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
        public static FatherController Instance { private set; get; }
        SoundEffect jumpSound;
        public override void Initialize() {
            base.Initialize();
            Instance = this;
            jumpSound = KGame.GetContent<SoundEffect>("Sound/jumpFather");
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
            jumpSound.Play(0.2f, (float)ShrimpfulGame.Random.NextDouble()*0.4f-0.5f, 0);
        }
        void Switch() {
            ac.SetAnimation("Childless");
            controlling = false;
            BabyController.Instance.GameObject.active = true;
            BabyController.Instance.justSpawned = true;
            BabyController.Instance.Transform.Position = Transform.Position;
            BabyController.Instance.velocity = velocity;
            BabyController.Instance.UpdateCamera();
        }

        internal override void UpdateCamera() {
            base.UpdateCamera();
            Camera.MainCamera.Size = 42f;
            Camera.MainCamera.Transform.Position = new Vector2(20.5f, -12.5f);
        }
    }
}
