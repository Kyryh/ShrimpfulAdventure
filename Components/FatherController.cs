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
            if (controlling && Input.InteractPressed()) {
                Switch();
            }
            base.Update(deltaTime);
        }

        void Switch() {
            controlling = false;
            baby.GameObject.active = true;
            baby.justSpawned = true;
        }
    }
}
