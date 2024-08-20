using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Rock : Component {
        float yVelocity = 0f;
        public override void Initialize() {
            base.Initialize();
            GameObject.GetComponent<Collider>().OnCollision += Rock_OnCollision;
        }

        private void Rock_OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (hitInfo.direction == GameConstants.Vector2.Down)
                yVelocity = 0;
        }

        public override void Update(float deltaTime) {
            yVelocity -= 0.01f;
            Transform.Position += new Vector2(0, yVelocity);
        }
    }
}
