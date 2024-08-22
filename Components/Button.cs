using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Button : Component {
        SpriteRenderer sr;
        bool wasPressed = false;
        bool isPressed = false;
        public Action OnPress { private get; init; }
        public Action OnDepress { private get; init; }
        public override void Initialize() {
            base.Initialize();
            GameObject.GetComponent<Collider>().OnCollision += Button_OnCollision;
            sr = GameObject.GetComponent<SpriteRenderer>();
        }

        private void Button_OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (other.GameObject.Name == "Shrimp" || other.GameObject.Name == "Rock") {
                isPressed = true;
            }
        }

        public override void Update(float deltaTime) {
            base.Update(deltaTime);
            if (wasPressed == isPressed) {
                isPressed = false;
                return;
            }
            wasPressed = isPressed;
            if (isPressed) {
                OnPress();
                KGame.GetContent<SoundEffect>("Sound/lever").Play(0.2f, (float)new Random().NextDouble() * 0.4f - 0.5f, 0);
                sr.spriteIndex = 1;
            } else {
                OnDepress();
                sr.spriteIndex = 0;
            }
            isPressed = false;
        }
    }
}
