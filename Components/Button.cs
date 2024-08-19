using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
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
                sr.spriteIndex = 1;
            } else {
                OnDepress();
                sr.spriteIndex = 0;
            }
            isPressed = false;
        }
    }
}
